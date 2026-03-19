using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Repositories.Interfaces;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly WarehouseDbContext _context;

    public WarehouseRepository(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task<Warehouse?> GetById(Guid id)
    {
        return await _context.Warehouses.FindAsync(id);
    }

    public async Task<Warehouse[]> GetAll(int skip = 0, int take = 100)
    {
      return await _context.Warehouses
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToArrayAsync();
    }

    public async Task<Warehouse> Add(Warehouse warehouse)
    {
        _context.Warehouses.Add(warehouse);
        await _context.SaveChangesAsync();
        return warehouse;
    }

    public async Task Update(Warehouse warehouse)
    {
        if (warehouse.Id == Guid.Empty)
        {
            throw new ArgumentException("Warehouse ID cannot be empty for update.");
        }
        _context.Warehouses.Update(warehouse);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse != null)
        {
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}