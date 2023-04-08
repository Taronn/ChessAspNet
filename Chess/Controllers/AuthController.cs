using System.Security.Claims;
using Chess.DTOs;
using Chess.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Chess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService;
        private readonly IMemoryCache _cache;

        public AuthController(IAuthService authService, IGameService gameService, IPlayerService playerService, IMemoryCache cache)
        {
            _authService = authService;
            _gameService = gameService;
            _playerService = playerService;
            _cache = cache;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AuthResult result = await _authService.AuthenticateUserAsync(loginDto, HttpContext);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserAsync()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            GameDto? game = _gameService.GetGameByUserId(userId);
            PlayerDto player = await _playerService.CreatePlayerAsync(userId, game);
            return Ok(player);
        }
    }
}
