using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs;

public class ChessHub : Hub
{
    public async Task Send(string message)
    {
        await Clients.All.SendAsync("Receive", message);
    }
}