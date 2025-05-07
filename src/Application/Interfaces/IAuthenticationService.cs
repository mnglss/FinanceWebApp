

using Application.Models.Request;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(RegisterRequest request);
    Task<string> LoginAsync(LoginRequest request);
    Task LogoutAsync();
    //Task<AuthenticationResponse> RefreshTokenAsync(string token);
}
