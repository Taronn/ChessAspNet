using Chess.Application.Interfaces.Caching;
using Chess.Infrastructure.Caching;
using Chess.Infrastructure.Factories;

namespace Chess.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IGameCache, GameCache>();
        services.AddSingleton<IInviteCache, InviteCache>();

        return services;
    }
}