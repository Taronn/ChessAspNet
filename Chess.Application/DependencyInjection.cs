using Chess.Application.Interfaces.Services;
using Chess.Application.Services;

namespace Chess.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}