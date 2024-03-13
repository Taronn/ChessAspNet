using Chess.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Chess.Domain.Entities;
using System.ComponentModel;

namespace Chess.API.Hubs;

[Authorize]
public class ChessHub : Hub
{
    private string UserId => Context.User!.FindFirst("Id")!.Value;
    private const string Lobby = "Lobby";
    /*Guid guidId = Guid.Parse(UserId);
    private Player Player => _playerService.GetPlayer(Context.UserIdentifier!)!;
    private Game? Game => _gameService.GetGameByUserId(Player);*/
    private readonly IGameService _gameService;
    private readonly IPlayerService _playerService;


    public ChessHub(IPlayerService playerService, IGameService gameService)
    {
        _playerService = playerService;
        _gameService = gameService;
    }


    public override async Task OnConnectedAsync()
    {
        System.Console.WriteLine("adasd");

        await Groups.AddToGroupAsync(Context.ConnectionId, Lobby);
        await Clients.User(UserId).SendAsync("GetPlayersList", "Hello from server!");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        System.Console.WriteLine("Disconnected");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, Lobby);
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