using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Services.Repositories.Interfaces
{
    public interface IProductRepository
    {    Task<Product?> GetById(Guid id);
        Task<Product[]> GetAll(int skip = 0, int take = 100);
        Task<Product> Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}