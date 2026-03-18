using WarehouseStorage.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class WarehouseDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Product> Products => Set<Product>();

    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transit> Transits { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // IMPORTANT
        builder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
    }
}
