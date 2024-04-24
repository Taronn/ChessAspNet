using Chess.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Chess.Domain.Entities;
using Chess.Application.Interfaces.Caching;
using System.ComponentModel;
namespace Chess.API.Hubs;

[Authorize]
public class ChessHub : Hub
{
    private Guid UserId => Guid.Parse(Context.User!.FindFirst("Id")!.Value);
    private readonly IGameService _gameService;
    private readonly IPlayerService _playerService;
    private readonly IInviteService _inviteService;
    private readonly IInviteCache _inviteCache;

    public ChessHub(IPlayerService playerService, IGameService gameService, IInviteService inviteService, IInviteCache inviteCache)
    {
        _playerService = playerService;
        _gameService = gameService;
        _inviteService = inviteService;
        _inviteCache = inviteCache;
    }

    public async Task InvitePlayer(Invite invite)
    {
        try
        {
            _inviteService.Save(UserId,invite);
            Invite newInvite = _inviteCache.Find(invite.ToId);
            await Clients.User(invite.ToId.ToString()).SendAsync("InviteReceived", newInvite);
        }
        catch
        {
            await Clients.User(UserId.ToString()).SendAsync("Error", "INVITE_PLAYER");
        }
    }

    public override async Task OnConnectedAsync()
    {
        Player[] players = _playerService.FindAll();
        Player player = await _playerService.Join(UserId);
        await Clients.User(UserId.ToString()).SendAsync("GetPlayersList", players);
        await Clients.Others.SendAsync("PlayerJoin", player);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        _playerService.Remove(UserId);
        await Clients.Others.SendAsync("PlayerLeave", UserId);
    }

    public async Task AcceptInvite()
    {
        try
        {
            Game game = _inviteService.Accept(UserId);
            await Clients.Users(game.WhitePlayerId.ToString(),game.BlackPlayerId.ToString()).SendAsync("StartGame", game);
            await Clients.All.SendAsync("GameStarted", game);
        }
        catch
        {
            await Clients.User(UserId.ToString()).SendAsync("Error", "ACCEPT_INVITE");
        }
    }
    public async Task RejectInvite()
    {
        Invite invite = _inviteService.Reject(UserId);
        await Clients.User(UserId.ToString()).SendAsync("InviteRejected", invite);
    }

    public async Task SendMessage(string message)
    {
        var player = _playerService.Find(UserId);
        // Broadcast the received message to all clients
        await Clients.Others.SendAsync("ReceiveMessage", player, message);
    }

    // for testing purposes only - remove later
    public async Task MakeMove(string from, string to, string opponentId)
    {
        System.Console.WriteLine($"Move from {from} to {to} {opponentId}");
        System.Console.WriteLine(opponentId);
        System.Console.WriteLine(UserId);
        await Clients.User(opponentId).SendAsync("MakeMove", from, to);
    }
    /*public async Task MakeMove(string from, string to)
    {
        if (Game == null)
        {
            return;
        }

        MoveResultType moveResult = _gameService.MakeMove(Player, Game, from, to);
        switch (moveResult)
        {
            case MoveResultType.ValidMove:
                await Clients.User(Opponent?.Id ?? "").SendAsync("MakeMove", from, to);
                break;
            case MoveResultType.EndGame:
                await Clients.User(Opponent?.Id ?? "").SendAsync("MakeMove", from, to);
                await HandleEndGame();
                break;
            case MoveResultType.InvalidMove:
                await Clients.Caller.SendAsync("InvalidMove");
                break;
        }
    }*/
}