public record RegisterRequest(string username, string Password);

public record AuthResponse(string Token);

public record LoginRequest(string username, string Password);