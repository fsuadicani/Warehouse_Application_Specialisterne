using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Services.Repositories.Interfaces
{
    public interface ITransitRepository
    {
        Task<Transit> CreateAsync(Transit transit);
        Task UpdateAsync(Transit transit);
        Transit? Get(Guid transitId);
    }
}