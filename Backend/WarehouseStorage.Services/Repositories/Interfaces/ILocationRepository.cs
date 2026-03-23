using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Services.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Location GetLocationWithStock(Guid id);
    }
}