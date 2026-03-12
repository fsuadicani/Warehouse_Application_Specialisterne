using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WarehouseStorage.Infrastructure;

public class WarehouseDbContextFactory 
    : IDesignTimeDbContextFactory<WarehouseDbContext>
{
    public WarehouseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WarehouseDbContext>();

        optionsBuilder.UseNpgsql(ConnectionString.GetString());

        return new WarehouseDbContext(optionsBuilder.Options);
    }
}