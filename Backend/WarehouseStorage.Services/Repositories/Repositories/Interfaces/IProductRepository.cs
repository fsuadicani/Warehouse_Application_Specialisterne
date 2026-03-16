using WarehouseStorage.Domain.Models;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product[]> GetAllAsync();
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}