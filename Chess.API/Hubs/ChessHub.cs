using Chess.Application.Interfaces.Services;
using Chess.Application.Services;
using Chess.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;


namespace Chess.API.Hubs;

[Authorize]
public class ChessHub : Hub
{
    private string UserId => Context.User!.FindFirst("Id")!.Value;
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
        Console.WriteLine("adasd");
        Player[] playersList = _playerService.FindAll();

        await Groups.AddToGroupAsync(Context.ConnectionId, Lobby);
        await Clients.User(UserId).SendAsync("GetPlayersList", "Hello from server!");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        System.Console.WriteLine("Disconnected");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, Lobby);
    }

    public async Task Send(string message)
    {
        await Clients.All.SendAsync("Receive", message);
    }
}



/*public override async Task OnConnectedAsync()
{
    string userId = Context.UserIdentifier!;
    GameDto? game = _gameService.GetGameByUserId(userId);
    PlayerDto player = await _playerService.CreatePlayerAsync(userId, game);

    if (game == null)
    {

        await Clients.Users(AvailablePlayers.Where(p => p.Id != Player.Id).Select(p => p.Id)).SendAsync("PlayerJoined", Player);
        await Clients.Caller.SendAsync("GetPlayersList", AvailablePlayers.Where(p => p.Id != Player.Id));
        return;
    }

    await Clients.Caller.SendAsync("SetGame", game.Board.ToPgn(), game.WhitePlayer, game.BlackPlayer);

    if (Opponent == null)
    {
        await Clients.Caller.SendAsync("OpponentDisconnected");
    }
    else
    {
        await Clients.User(Opponent.Id).SendAsync("OpponentConnected");
    }

}*/
