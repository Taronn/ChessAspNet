using Chess.Domain.Entities;
using Chess.Domain.Enums;

namespace Chess.Infrastructure.Repositories;

public class StatisticRepository : IStatisticRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public StatisticRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Statistic> UpdateAsync(Statistic statistic)
    {
        const string query = @"UPDATE ""Statistics"" 
                           SET ""Wins"" = @Wins,
                               ""Losses"" = @Losses,
                               ""Draws"" = @Draws,
                               ""Rating"" = @Rating,
                               ""GamesPlayed"" = @GamesPlayed
                           WHERE ""TypeId"" = @TypeId AND ""UserId"" = @UserId";
        var connection = _sqlConnectionFactory.Create();
        await connection.OpenAsync();
        await connection.ExecuteAsync(query, statistic);
        await connection.CloseAsync();
        return statistic;
    }
}