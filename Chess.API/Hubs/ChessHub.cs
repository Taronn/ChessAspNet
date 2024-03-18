using Chess.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Chess.Domain.Entities;
using System.ComponentModel;
using Chess.Application.Services;

namespace Chess.API.Hubs;

/*[Authorize]
*/public class ChessHub : Hub
{
    private Guid UserId => Guid.Parse(Context.User!.FindFirst("Id")!.Value);
    private const string Lobby = "Lobby";
    private readonly IGameService _gameService;
    private readonly IPlayerService _playerService;

    public ChessHub(IPlayerService playerService, IGameService gameService)
    {
        _playerService = playerService;
        _gameService = gameService;
    }


    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("User");
        Game game = _gameService.Find(UserId);
        Player player = await _playerService.Join(UserId, game);
        Console.WriteLine(player.Username+ "Connected");
        await Groups.AddToGroupAsync(Context.ConnectionId, Lobby);
        await Clients.User(UserId.ToString()).SendAsync("GetPlayersList", player.Username+"Connected");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Player player = _playerService.Find(UserId);
        Console.WriteLine(player.Username+"Disconnected");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, Lobby);
        await Clients.User(UserId.ToString()).SendAsync("GetPlayersList", player.Username + "Disconnected");
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