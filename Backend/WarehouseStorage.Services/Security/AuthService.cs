using BCrypt.Net;
using WarehouseStorage.Domain.Enums;
using WarehouseStorage.Domain.Exceptions;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Security.Interfaces;

namespace WarehouseStorage.Services.Security;
public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwt;

    public AuthService(IUserRepository userRepository,
                       IJwtTokenGenerator jwt)
    {
        _userRepository = userRepository;
        _jwt = jwt;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await _userRepository.GetByUsernameAsync(request.username);
        if (existing != null)
            throw new UserAlreadyExistsException("User already exists");

        var user = new ApplicationUser
        {
            UserName = request.username,
        };

        await _userRepository.AddAsync(user, request.Password);
        await _userRepository.AddToRoleAsync(user, Role.EMPLOYEE); //Default Employee

        var token = await _jwt.GenerateTokenAsync(user);

        return new AuthResponse(token);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.username);

        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new InvalidCredentialsException("Invalid credentials");

        var token = await _jwt.GenerateTokenAsync(user);

        return new AuthResponse(token);
    }
}