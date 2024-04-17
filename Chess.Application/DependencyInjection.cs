using Chess.Application.Interfaces.Services;
using Chess.Application.Services;

namespace Chess.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IFriendsService, FriendsService>();
        services.AddScoped<IInviteService, InviteService>();
        return services;
    }
}