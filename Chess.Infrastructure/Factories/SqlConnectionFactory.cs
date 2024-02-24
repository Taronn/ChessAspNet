namespace Chess.Infrastructure.Factories;

public class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("PostgresConnection")!;

    public NpgsqlConnection Create()
    {
        return new NpgsqlConnection(_connectionString);
    }
}