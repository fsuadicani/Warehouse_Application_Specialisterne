using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Domain.Models;

public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
    }
}
