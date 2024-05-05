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

    public async Task<Statistic> AddAsync(Statistic statistic)
    {
        const string query = @"INSERT INTO ""Statistic"" (""TypeId"",""Wins"",""Losses"",""Draws"",""Rating"",""GamesPlayed"",""UserId"")
                                        VALUES (@TypeId,@Wins,@Losses,@Draws,@Rating,@GamesPlayed,@UserId)";
        var connection = _sqlConnectionFactory.Create();
        await connection.OpenAsync();
        await connection.ExecuteAsync(query, statistic);
        await connection.CloseAsync();
        return statistic;
    }
}