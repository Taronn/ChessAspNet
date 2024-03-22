using Chess.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Chess.Domain.Entities;
using System.ComponentModel;

namespace Chess.API.Hubs;

[Authorize]
public class ChessHub : Hub
{
    private Guid UserId => Guid.Parse(Context.User!.FindFirst("Id")!.Value);
    private readonly IGameService _gameService;
    private readonly IPlayerService _playerService;


    public ChessHub(IPlayerService playerService, IGameService gameService)
    {
        _playerService = playerService;
        _gameService = gameService;
    }


    public override async Task OnConnectedAsync()
    {
/*        Game game = _gameService.Find(UserId);
 *      
*/     
        Player[] players = _playerService.FindAll();
        Player player = await _playerService.Join(UserId);
        await Clients.User(UserId.ToString()).SendAsync("GetPlayersList", players);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        _playerService.Remove(UserId);
        await Clients.User(UserId.ToString()).SendAsync("GetPlayersList", "Users");

    }

    // for testing purposes only - remove later
    public async Task Send(string message)
    {
        await Clients.All.SendAsync("Receive", message);
    }

    // for testing purposes only - remove later
    public async Task MakeMove(string from, string to, string opponentId)
    {
        System.Console.WriteLine($"Move from {from} to {to} {opponentId}");
        System.Console.WriteLine(opponentId);
        System.Console.WriteLine(UserId);
        await Clients.User(opponentId).SendAsync("MakeMove", from, to);
    }
}