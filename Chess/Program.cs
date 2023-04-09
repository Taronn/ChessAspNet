using Chess.Models;
using Chess.Profiles;
using Chess.Repositories;
using Chess.Services;
using Chess.Hubs;
using Chess.Validators;
using Chess.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);
var app = builder.Build();
Configure(app);


app.MapHub<ChessHub>("/hubs/chess");


app.Run();

void RegisterServices(IServiceCollection services)
{
    string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<ChessContext>(options => options.UseSqlServer(connection));


    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "auth";
            options.LoginPath = string.Empty;
            options.LogoutPath = string.Empty;
            options.ExpireTimeSpan = TimeSpan.FromDays(14);
            options.SlidingExpiration = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
        });
    services.AddControllers();
    services.AddAuthorization();
    services.AddMemoryCache();
    services.AddScoped<IMemoryCache, MemoryCache>();
    services.AddSignalR();


    services.AddAutoMapper(typeof(ChessProfile));
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IRegistrationService, RegistrationService>();
    services.AddScoped<ICacheService, CacheService>();
    services.AddScoped<IPlayerService, PlayerService>();
    services.AddScoped<IGameService, GameService>();
    services.AddScoped<IChallengeService, ChallengeService>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IGameRepository, GameRepository>();
    services.AddScoped<IStatsRepository, StatsRepository>();

    services.AddScoped<IValidator<RegistrationDto>, RegistrationDtoValidator>();
    services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();

}

void Configure(WebApplication app)
{
    app.UseCors(options => options.WithOrigins("https://chesstaron.netlify.app").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
    app.UseStatusCodePages();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}
