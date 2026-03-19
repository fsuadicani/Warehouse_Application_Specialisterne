using WarehouseStorage.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class WarehouseDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transit> Transits { get; set; }

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Location> Locations => Set<Location>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // IMPORTANT
        builder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
    }
}
