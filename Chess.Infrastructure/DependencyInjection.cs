using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Services;
using Chess.Infrastructure.Caching;
using Chess.Infrastructure.Factories;

namespace Chess.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Factories
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        // Repositories
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IFriendsRepository, FriendsRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IStatisticRepository, StatisticRepository>();
        // Caching
        services.AddSingleton<IGameCache, GameCache>();
        services.AddSingleton<IInviteCache, InviteCache>();
        services.AddSingleton<IPlayerCache, PlayerCache>();

        return services;
    }
}