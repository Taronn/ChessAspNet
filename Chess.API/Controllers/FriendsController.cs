using System.Net;
using Chess.Application.Interfaces.Services;
using Chess.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers;

public class FriendsController : Controller
{
    private Guid UserId => Guid.Parse(User.FindFirst("Id")!.Value);

    private readonly IUserService _userService;
    private readonly IFriendsService _friendsService;

    public FriendsController(
        IUserService userService,
        IFriendsService friendsService)
    {
        _userService = userService;
        _friendsService = friendsService;
    }

    [HttpPost("send-request")]
    [Authorize]
    public async Task<IActionResult> SendFriendRequest(Guid receiverId)
    {
        try
        {
            var friendRequest = await _friendsService.SendRequest(UserId, receiverId);
            return Ok(friendRequest);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("response")]
    [Authorize]
    public async Task<IActionResult> UserResponse(Guid friendRequestId, FriendRequestStatus responseStatus)
    {
        try
        {
            var friendRequest = await _friendsService.UserResponse(UserId, friendRequestId, responseStatus);
            return Ok(friendRequest);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}