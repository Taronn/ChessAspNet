using Chess.DTOs;

namespace Chess.Services
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateUserAsync(LoginDto loginDto, HttpContext httpContext);
    }
}
