using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs;

[Authorize]
public class ChessHub : Hub
{
    private string UserId => Context.User!.FindFirst("Id")!.Value;
    private const string Lobby = "Lobby";
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, Lobby);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, Lobby);
    }

    public async Task Send(string message)
    {
        await Clients.All.SendAsync("Receive", message);
    }
}