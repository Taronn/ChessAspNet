using Chess.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Chess.Domain.Entities;
using System.ComponentModel;
using Chess.Domain.Enums;
using System.Text.Json;
using System;
namespace Chess.API.Hubs;

[Authorize]
public class ChessHub : Hub
{
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