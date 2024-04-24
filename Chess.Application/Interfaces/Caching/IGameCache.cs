namespace Chess.Application.Interfaces.Caching;

public interface IGameCache
{
    Game Find(Guid id);
    void Add(Game game);
    void Remove(Guid id);
}