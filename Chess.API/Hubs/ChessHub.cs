using Chess.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Chess.Domain.Entities;
using System.ComponentModel;
using Chess.Domain.Enums;
using System.Text.Json;
using System;
using static Chess.Application.Services.InviteService;
namespace Chess.API.Hubs;

[Authorize]
public class ChessHub : Hub
{
    private Player Player => _playerService.Find(UserId);
/*    private Player Opponent => _gameService.GetOpponent(Player);
*/    private Player Inviter => _inviteService.FindInviter(UserId);
/*    private Game Game => _gameService.Find(Player);
*/    private Invite Invite => _inviteService.FindInvite(Player);
/*    private IEnumerable<PlayerDto> AvailablePlayers => _playerService.GetAvailablePlayers();
*/    



    private Guid UserId => Guid.Parse(Context.User!.FindFirst("Id")!.Value);
    private readonly IGameService _gameService;
    private readonly IPlayerService _playerService;
    private readonly IInviteService _inviteService;


    public ChessHub(IPlayerService playerService, IGameService gameService, IInviteService inviteService)
    {
        _playerService = playerService;
        _gameService = gameService;
        _inviteService=inviteService;
    }

    public async Task InvitePLayer(string toId,Invite invite)
    {
        Guid ToId=Guid.Parse(toId);
        Invite newInvite = _inviteService.Save(UserId, ToId, invite);
        await Clients.User(toId).SendAsync("InviteReceived", newInvite);
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

    public async Task AcceptChallenge()
    {
        InviteResult result = _inviteService.AcceptChallenge(Player,UserId);

        switch (result)
        {
            case InviteResult.NoChallengeExists:
                await Clients.Caller.SendAsync("ChallengeError", "Challenge does not exist.");
                break;

            case InviteResult.ChallengerOffline:
                await Clients.Caller.SendAsync("ChallengeError", "Challenger is offline.");
                break;

            case InviteResult.ChallengerAlreadyInGame:
                await Clients.Caller.SendAsync("ChallengeError", "Challenger is already in a game.");
                break;

            case InviteResult.Success:
/*                await Clients.Users(Game!.WhitePlayer.Id, Game.BlackPlayer.Id).SendAsync("ChallengeAccepted", Game.Id, Game.WhitePlayer.Username, Game.BlackPlayer.Username);
*//*                await Clients.Users(AvailablePlayers.Select(p => p.Id)).SendAsync("GameStarted", Game.WhitePlayer, Game.BlackPlayer);
*/                break;
        }

    }
    public async Task RejectChallenge(string toId)
    {
        Guid ToId = Guid.Parse(toId);
        _inviteService.RemoveInvite(ToId);
        await Clients.User(toId).SendAsync("InviteRejected", Player.Username);
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