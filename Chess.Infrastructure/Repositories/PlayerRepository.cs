using Chess.Domain.Enums;

namespace Chess.Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    
    public PlayerRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async void AddAsync(User user)
    {
        const string query = @"INSERT INTO ""Users"" (""Username"", ""Password"", ""Email"") VALUES (@Username, @Password, @Email)";
        var connection = _sqlConnectionFactory.Create();
        await connection.OpenAsync();
        await connection.ExecuteAsync(query, user);
        await connection.CloseAsync();
    }

    public async Task<Player> FindAsync(Guid id)
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
        var player = await connection.QueryAsync<Player, Statistic, Player>(
            query,
            (player, statistic) =>
            {
                if (!playerDictionary.TryGetValue(player.Id, out var currentPlayer))
                {
                    currentPlayer = player;
                    currentPlayer.Statistics = new List<Statistic>();
                    playerDictionary.Add(player.Id, currentPlayer);
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