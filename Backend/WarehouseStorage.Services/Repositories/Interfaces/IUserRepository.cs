using WarehouseStorage.Domain.Enums;
using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Services.Repositories.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByUsernameAsync(string username);
    Task<ApplicationUser> AddAsync(ApplicationUser user, string password);

    Task<bool> AddToRoleAsync(ApplicationUser user, Role role);

    Task<IList<string>> GetRolesAsync(ApplicationUser user);
}