using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Domain.Enums;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Repositories.Interfaces;

namespace WarehouseStorage.Services.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ApplicationUser?> GetByUsernameAsync(string username)
    {
        return await _userManager.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<ApplicationUser> AddAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to create user: {errors}");
        }

        return user;
    }

    public async Task<bool> AddToRoleAsync(ApplicationUser user, Role role)
    {
        var roleName = role.ToString();
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var createResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            // If creation failed and role still doesn't exist, we have a real problem
            if (!createResult.Succeeded && !await _roleManager.RoleExistsAsync(roleName))
            {
                return false;
            }
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded;
    }
    public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}