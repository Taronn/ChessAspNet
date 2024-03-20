using Chess.Domain.Enums;

namespace Chess.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    
    public UserRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async void AddAsync(User user)
    {
        const string query = """INSERT INTO users (id, username, password, email) VALUES (@Id, @Username, @Password, @Email)""";
        var connection = _sqlConnectionFactory.Create();
        await connection.OpenAsync();
        await connection.ExecuteAsync(query, user);
        await connection.CloseAsync();
    }

    public async Task<User> FindAsync(Guid id)
    {
        const string query = $"""
                             SELECT * 
                             FROM "Users" 
                             INNER JOIN "Settings" ON "Users"."Id" = "Settings"."Id"
                             WHERE "Users"."Id" = @Id
                             """;
        var param = new { Id = id };
        await using var connection = _sqlConnectionFactory.Create();
        var user = await connection.QueryAsync<User, Setting, User>(query, (user, settings) =>
        {
            user.Settings = settings;
            return user;
        }, 
            param: param,
            splitOn: "Id");
        return user.FirstOrDefault();
    }

    public async Task<Player> FindPlayerAsync(Guid id)
    {
        const string query = @"
                        SELECT *
                        FROM ""Users""
                        LEFT JOIN ""Statistics"" ON ""Users"".""Id"" = ""Statistics"".""UserId""
                        WHERE ""Users"".""Id"" = @Id
                        ";
        var param = new { Id = id };
        await using var connection = _sqlConnectionFactory.Create();
        var playerDictionary = new Dictionary<Guid, Player>();
        var player = await connection.QueryAsync<User, Statistic, Player>(
            query,
            (user, statistic) =>
            {
                if (!playerDictionary.TryGetValue(user.Id, out var currentPlayer))
                {
                    currentPlayer = new Player
                    {
                        Id = user.Id,
                        Username = user.Username,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        Country = user.Country,
                        Picture = user.Picture,
                        Statistics = new List<Statistic>()
                    };
                    playerDictionary.Add(user.Id, currentPlayer);
                }

                if (statistic != null)
                {
                    currentPlayer.Statistics.Add(statistic);
                }

                return currentPlayer;
            },
            param,
            splitOn: "Id"
        );

        return player.FirstOrDefault();
    }
}