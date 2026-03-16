
using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Services.Security.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
}