using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseStorage.Infrastructure;
using WarehouseStorage.Services.Interfaces;
using WarehouseStorage.Services.Repositories;
using WarehouseStorage.Services.Repositories.Interfaces;
using WarehouseStorage.Services.Security;
using WarehouseStorage.Services.Security.Interfaces;

namespace WarehouseStorage.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ConnectionString.Initialize(configuration);

        services.AddDbContext<WarehouseDbContext>(options =>
            options.UseNpgsql(ConnectionString.GetString()));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ITransitRepository, TransitRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<ITransitService, TransitService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();


        return services;
    }
}