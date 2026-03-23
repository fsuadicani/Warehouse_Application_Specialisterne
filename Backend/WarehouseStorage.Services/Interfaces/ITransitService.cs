using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Services.Interfaces
{
    public interface ITransitService
    {
        Task StartTransitToDestination(Transit transit);

        Task TransitHasArrived(Guid transitID);
    }
}