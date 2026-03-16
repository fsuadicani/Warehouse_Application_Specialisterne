using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Security.Interfaces;

namespace WarehouseStorage.Services.Security;

public class JwtTokenGenerator : IJwtTokenGenerator{
    private readonly IConfiguration _config;

    private readonly UserManager<ApplicationUser> _userManager;

    public JwtTokenGenerator(IConfiguration config, UserManager<ApplicationUser> userManager)
    {
        _config = config;
        _userManager = userManager;
    }

    public async Task<string> GenerateTokenAsync(ApplicationUser user){
            var jwtKey = _config["Jwt:Key"] 
                ?? throw new InvalidOperationException("JWT key is not configured.");
            
            if (Encoding.UTF8.GetByteCount(jwtKey) < 32)
                throw new InvalidOperationException("JWT key must be at least 256 bits (32 bytes).");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));            
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}