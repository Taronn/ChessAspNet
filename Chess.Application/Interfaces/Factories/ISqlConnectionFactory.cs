namespace Chess.Application.Interfaces.Factories;

public interface ISqlConnectionFactory
{
    NpgsqlConnection Create();
}