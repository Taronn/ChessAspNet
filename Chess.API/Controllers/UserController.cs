using System.Net;
using Chess.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private string UserId => User.FindFirst("Id")!.Value;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        System.Console.WriteLine(HttpContext.User.FindFirst("Id")!.Value);
        var user = await _userService.FindAsync(id);
        return Ok(User.FindFirst("Username")?.Value);
    }
}