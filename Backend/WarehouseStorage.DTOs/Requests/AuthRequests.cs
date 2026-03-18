public record RegisterRequest(string Username, string Password);

public record AuthResponse(string Token);

public record LoginRequest(string Username, string Password);
