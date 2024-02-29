namespace Chess.Application.Interfaces.Caching;

public interface IGameCache
{
    Game FindById(int id);
    Game FindByPlayerId(int id);
    void Add(Game game);
}