namespace Chess.Application.Interfaces.Caching;

public interface IPlayerCache
{
    Player Find(Guid id);
    Player[] FindAll();
    void Add(Player player);
    void Remove(Guid id);
}