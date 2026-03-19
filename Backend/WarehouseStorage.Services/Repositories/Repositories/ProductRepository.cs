using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Repositories.Interfaces;


namespace WarehouseStorage.Services.Repositories.Repositories
{   
    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseDbContext _context;

        public ProductRepository(WarehouseDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product[]> GetAll(int skip = 0, int take = 100)
        {
            return await _context.Products
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task<Product> Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            if (product.Id == Guid.Empty)
            {
                throw new ArgumentException("Product ID cannot be empty for update.");
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}