using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Repositories.Interfaces;

namespace WarehouseStorage.Services.Repositories.Repositories
{
    public class WarehouseRepository : IWarehouse
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
            .OrderBy(w => w.Id)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync();
        }

        public async Task<Warehouse> Add(Warehouse warehouse)
        {
            if (warehouse == null)
            {
                throw new ArgumentNullException(nameof(warehouse));
            }
            
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task Update(Warehouse warehouse)
        {
            if (warehouse == null)
            {
                throw new ArgumentNullException(nameof(warehouse));
            }

            if (warehouse.Id == Guid.Empty)
            {
                throw new ArgumentException("Warehouse ID cannot be empty for update.");
            }

            // Ensure we don't track multiple instances of the same Address (or other owned entities) during updates.
            // This can happen when a caller replaces a nested entity instance with the same key value.
            var address = warehouse.Location?.Address;
            if (address != null)
            {
                var trackedAddress = _context.ChangeTracker.Entries<Address>()
                    .FirstOrDefault(e => e.Entity.Id == address.Id && !ReferenceEquals(e.Entity, address));

                if (trackedAddress != null)
                {
                    trackedAddress.State = EntityState.Detached;
                }
            }

            _context.Warehouses.Update(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                throw new KeyNotFoundException($"Warehouse with id '{id}' not found");
            }
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}