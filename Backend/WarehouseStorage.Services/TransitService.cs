using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.DTOs.DataTransferObjects;
using WarehouseStorage.Services.Factories;
using WarehouseStorage.Services.Interfaces;
using WarehouseStorage.Services.Repositories.Interfaces;

namespace WarehouseStorage.Services
{
    public class TransitService : ITransitService
    {
        private WarehouseDbContext _context;
        private IStockRepository _stockRepository;

        private ITransitRepository _transitRepository;

        public TransitService(WarehouseDbContext context, IStockRepository stockRepository, ITransitRepository transitRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
            _transitRepository = transitRepository;
        }
    
        public async Task StartTransitToDestination(Transit transit)
        {
            if (transit == null)
                throw new ApplicationException("No Transit Provided");
            if (transit.Location == null)
                throw new ApplicationException("A Transit must include a location");
            if (!transit.Location.Stocks.Any())
                throw new ApplicationException("A Transit must include some stocks to be send");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                Transit newTransit = await _transitRepository.CreateAsync(transit);

                if (IsAnInternalTransit(transit))
                {
                    await LoadTransitWithOriginStocks(transit);
                }

                await transaction.CommitAsync();
            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                throw e;
            }
        }

        public async Task TransitHasArrived(Guid transitID)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                Transit? transit = _transitRepository.Get(transitID);

                if(transit == null)
                    throw new ArgumentException("Transit not found");

                await UnLoadTransitToDestinationStocks(transit);

                await _transitRepository.UpdateAsync(transit);

                await transaction.CommitAsync();
            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                throw e;
            }
        }
        private bool IsAnInternalTransit(Transit transit)
        {
            return transit.OriginId != null;
        }
        private async Task LoadTransitWithOriginStocks(Transit transit)
        {
            IEnumerable<Stock> originStocks = _stockRepository.GetSameStocksFromLocation(transit.Location.Stocks, transit.OriginId);
            foreach (Stock originStock in originStocks)
            {
                Stock? change = transit.Location.Stocks.First(stock => stock.Product.Id == originStock.Product.Id);
                originStock.ChangeQuantityBy(-change.Quantity.value);
                await _stockRepository.UpdateAsync(originStock);
            }
        }

        private async Task UnLoadTransitToDestinationStocks(Transit transit)
        {
            IEnumerable<Stock> destinationStocks  = _stockRepository.GetSameStocksFromLocation(transit.Location.Stocks, transit.DestinationId);

            foreach (Stock deliverStock in transit.Location.Stocks)
            {
                Stock? destinationStock = destinationStocks.FirstOrDefault(destinationStock => destinationStock.ProductId == deliverStock.ProductId);
                if (destinationStock != null)
                {
                    int deltaChange = deliverStock.Quantity.value;
                    deliverStock.ChangeQuantityBy(-deltaChange);
                    destinationStock.ChangeQuantityBy(deltaChange);
                    await _stockRepository.UpdateAsync(destinationStock);
                    await _stockRepository.UpdateAsync(deliverStock);
                }
                else
                {
                    StockDTO ToBeDeliveredDTO = ModelFactory.CreateStockDTO(deliverStock);
                    Stock TobeDelivered = ModelFactory.CreateStock(ToBeDeliveredDTO);
                    TobeDelivered.ProductId = deliverStock.ProductId;
                    TobeDelivered.LocationId = transit.DestinationId;
                    await _stockRepository.CreateAsync(TobeDelivered);
                    deliverStock.ChangeQuantityBy(-deliverStock.Quantity.value);
                    await _stockRepository.UpdateAsync(deliverStock);
                }
            }
            transit.DeliveryStatus = Domain.Enums.DeliveryStatus.DELIVERED;
        }
    }

}