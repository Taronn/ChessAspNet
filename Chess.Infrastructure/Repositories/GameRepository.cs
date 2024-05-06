using Chess.Domain.Entities;
using Chess.Domain.Enums;

namespace Chess.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GameRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Game> AddAsync(Game game)
    {
        const string query = @"INSERT INTO ""Games"" (""WhitePlayerId"",""BlackPlayerId"",""TypeId"",""Pgn"",""StartTime"",""EndTime"",""WinnerId"",""Timer"",""TimerIncrement"")
                                        VALUES (@WhitePlayerId,@BlackPlayerId,@TypeId,@Pgn,@StartTime,@EndTime,@WinnerId,@Timer,@TimerIncrement)";
        var connection = _sqlConnectionFactory.Create();
        await connection.OpenAsync();
        await connection.ExecuteAsync(query, game);
        await connection.CloseAsync();
        return game;
    }
    public async Task<Game> FindAsync(Guid id)
    {
        const string query = @"
                        SELECT *
                        FROM ""Games""
                        LEFT JOIN ""Users"" ON ""Games"".""Id"" = ""Users"".""UserId""
                        WHERE ""Games"".""Id"" = @Id
                        ";
        var param = new { Id = id };
        await using var connection = _sqlConnectionFactory.Create();
        var game = await connection.QueryAsync<Game, Player, Game>(
            query,
            (game, player) =>
            {
                var currentGame = game;
                return currentGame;
            },
            param,
            splitOn: "Id"
        );

        return game.FirstOrDefault();
    }
}