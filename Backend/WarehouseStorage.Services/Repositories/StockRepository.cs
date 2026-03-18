using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Repositories.Interfaces;

namespace WarehouseStorage.Services.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly WarehouseDbContext _context;
        public StockRepository(WarehouseDbContext context)
        {
            _context = context;
        }
        public async Task<Stock> CreateAsync(Stock stock)
        {
                Stock createdstock = _context.Add(stock).Entity;
                await _context.SaveChangesAsync();
                return createdstock;
        }

        public async Task UpdateAsync(Stock stock)
        {
            if (stock.Id == null)
            {
                throw new ArgumentException("No id provided with stock.");
            }
            _context.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Stock>> GetSameStocksFromLocationAsync(IEnumerable<Stock> stocks, Guid locationId)
        {
            return _context.Stocks
                .Where(stock => stock.LocationId == locationId)
                .Where(stock => stocks.Any(transitStock => transitStock.Product.Id == stock.Product.Id))
                .ToList();
        }
    }
}