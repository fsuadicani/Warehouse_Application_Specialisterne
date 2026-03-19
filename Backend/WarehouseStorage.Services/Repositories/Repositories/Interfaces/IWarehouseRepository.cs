using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Services.Repositories.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<Warehouse?> GetById(Guid id);
        Task<Warehouse[]> GetAll(int skip = 0, int take = 100);
        Task<Warehouse> Add(Warehouse warehouse);
        Task Update(Warehouse warehouse);
        Task Delete(Guid id);
    }
}