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

    public async Task<User> FindAsync(int id)
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
}