using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs;

public sealed class UserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User!.FindFirst("Id")!.Value;
    }
}