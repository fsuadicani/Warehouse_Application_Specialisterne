using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WarehouseStorage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<WarehouseDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString(ConnectionString.GetString())));

        return services;
    }
}