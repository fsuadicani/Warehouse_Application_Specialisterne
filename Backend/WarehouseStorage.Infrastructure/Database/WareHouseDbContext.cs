using Microsoft.EntityFrameworkCore;

public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {
    }

    //Example
    // public DbSet<Item> Items { get; set; }
}