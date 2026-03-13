using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.IO;
using WarehouseStorage.Infrastructure;

public class WarehouseDbContextFactory 
    : IDesignTimeDbContextFactory<WarehouseDbContext>
{
    public WarehouseDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<WarehouseDbContextFactory>()
            .AddEnvironmentVariables()
            .Build();

        ConnectionString.Initialize(configuration);

        var optionsBuilder = new DbContextOptionsBuilder<WarehouseDbContext>();

        optionsBuilder.UseNpgsql(ConnectionString.GetString());

        return new WarehouseDbContext(optionsBuilder.Options);
    }
}