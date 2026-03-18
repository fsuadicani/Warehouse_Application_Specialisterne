using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Domain.Models;
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
            if (transit.Location.Stocks.Any())
                throw new ApplicationException("A Transit must include some stocks to be send");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                Transit newTransit = await _transitRepository.CreateAsync(transit);

                IEnumerable<Stock> originStocks = await _stockRepository.GetSameStocksFromLocationAsync(transit.Location.Stocks, transit.OriginId);
                foreach (Stock originStock in originStocks)
                {
                    Stock? change = transit.Location.Stocks.First(stock => stock.Product.Id == originStock.Product.Id);
                    originStock.ChangeQuantityBy(change.Quantity.value);
                    await _stockRepository.UpdateAsync(originStock);
                    await _stockRepository.CreateAsync(change);
                }
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }
    }
}