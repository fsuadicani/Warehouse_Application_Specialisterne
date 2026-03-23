using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Repositories.Interfaces;

namespace WarehouseStorage.Services.Repositories
{
    public class TransitRepository : ITransitRepository
    {
        private readonly WarehouseDbContext _context;
        public TransitRepository(WarehouseDbContext context)
        {
            _context = context;
        }
        public async Task<Transit> CreateAsync(Transit transit)
        {
                Transit createdTransit = _context.Add(transit).Entity;
                await _context.SaveChangesAsync();
                return createdTransit;
        }

        public async Task UpdateAsync(Transit transit)
        {
            if (transit.Id == null)
            {
                throw new ArgumentException("No id provided with product.");
            }
            _context.Update(transit);
            await _context.SaveChangesAsync();
        }

        public Transit? Get(Guid transitId)
        {
            return _context.Transits
                .Include(transit => transit.Location)
                    .ThenInclude(location => location.Stocks)
                .FirstOrDefault(transit => transit.Id == transitId);
        }
    }
}