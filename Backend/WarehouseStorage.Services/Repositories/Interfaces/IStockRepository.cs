using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Services.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> CreateAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        IEnumerable<Stock> GetSameStocksFromLocation(IEnumerable<Stock> stocks, Guid? locationId);
    }
}