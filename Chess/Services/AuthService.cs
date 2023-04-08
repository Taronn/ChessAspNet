using Chess.Models;
using Chess.Repositories;
using Chess.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using FluentValidation.Results;
using FluentValidation;

namespace Chess.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<LoginDto> _validator;

        public AuthService(IUserRepository userRepository, IValidator<LoginDto> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<AuthResult> AuthenticateUserAsync(LoginDto loginDto, HttpContext httpContext)
        {
            ValidationResult result = await _validator.ValidateAsync(loginDto);
            if (!result.IsValid)
            {
                return new AuthResult(false, result.Errors.First().ToString());
            }

            User? user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user == null || !user.ValidatePassword(loginDto.Password))
            {
                return new AuthResult(false, "Incorrect email address or password");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = loginDto.RememberMe,
            };

            await httpContext.SignInAsync(claimsPrincipal, authProperties);

            return new AuthResult(true, "Authentication successful");

        }

    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public AuthResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
