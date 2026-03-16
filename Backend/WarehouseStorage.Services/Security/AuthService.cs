using BCrypt.Net;
using WarehouseStorage.Domain.Enums;
using WarehouseStorage.Domain.Exceptions;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Repositories.Interfaces;
using WarehouseStorage.Services.Security.Interfaces;

namespace WarehouseStorage.Services.Security;
public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwt;
    private static readonly string DummyHash = BCrypt.Net.BCrypt.HashPassword("never-match-password", workFactor: 12);

    public AuthService(IUserRepository userRepository,
                       IJwtTokenGenerator jwt)
    {
        _userRepository = userRepository;
        _jwt = jwt;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await _userRepository.GetByUsernameAsync(request.Username);
        if (existing != null)
            throw new UserAlreadyExistsException("User already exists");

        var user = new ApplicationUser
        {
            UserName = request.Username,
        };

        await _userRepository.AddAsync(user, request.Password);
        await _userRepository.AddToRoleAsync(user, Role.EMPLOYEE); //Default Employee

        var token = await _jwt.GenerateTokenAsync(user);

        return new AuthResponse(token);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        
        // Always perform hash verification to prevent timing-based username enumeration
        var hashToVerify = user?.PasswordHash ?? DummyHash;
        var passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, hashToVerify);

        if (user == null || !passwordValid)
            throw new InvalidCredentialsException("Invalid credentials");

        var token = await _jwt.GenerateTokenAsync(user!);

        return new AuthResponse(token);
    }}