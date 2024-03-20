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
*/      Player player = await _playerService.Join(UserId);
        Player[] players = _playerService.FindAll();
        Console.WriteLine(player.Username+"Connected");
        for(int i=0;i< _playerService.FindAll().Length; i++)
        {
            Console.WriteLine(players[i].Username);
        }
        await Clients.User(UserId.ToString()).SendAsync("GetPlayersList", "User");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Player player = _playerService.Find(UserId);
        Player[] players = _playerService.FindAll();
        Console.WriteLine();
        Console.WriteLine(player.Username + "Disconnected");
        _playerService.Remove(UserId);
        for (int i = 0; i < _playerService.FindAll().Length; i++)
        {
            Console.WriteLine(players[i].Username);
        }
        if (_playerService.FindAll().Length==0)
        {
            Console.WriteLine("User chka");
            Console.WriteLine();
        }
        await Clients.User(UserId.ToString()).SendAsync("GetPlayersList", "User");

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