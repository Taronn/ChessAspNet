using Chess.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{    
    protected string LogedInUserId => User.FindFirst("Id")!.Value;
    protected int UserId => Int32.Parse(LogedInUserId);

    private readonly IUserService _userService;
    private readonly IFriendsService _friendsService;
    
    public UserController(
        IUserService userService, 
        IFriendsService friendsService)
    {
        _userService = userService;
        _friendsService = friendsService;
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _userService.FindAsync(id);
        return Ok(User.FindFirst("Username")?.Value);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SendFriendRequest(int recieverId)
    {
        var user = await _friendsService.SendRequest(UserId, recieverId);
        return Ok(user);
    }
}