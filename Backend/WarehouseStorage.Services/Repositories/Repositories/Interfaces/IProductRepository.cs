using WarehouseStorage.Domain.Models;

public interface IProductRepository
{
    Task<Product?> GetById(Guid id);
    Task<Product[]> GetAll();
    Task<Product> Add(Product product);
    Task Update(Product product);
    Task Delete(Guid id);
}