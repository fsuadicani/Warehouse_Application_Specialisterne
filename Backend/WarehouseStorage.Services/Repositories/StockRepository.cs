using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Stock> GetSameStocksFromLocation(IEnumerable<Stock> stocks, Guid? locationId)
        {
            var productIds = stocks
                .Select(s => s.ProductId)
                .ToList();

            return _context.Stocks
                .Where(stock => stock.LocationId == locationId)
                .Where(stock => productIds.Contains(stock.ProductId))
                .ToList();
        }

    }
}