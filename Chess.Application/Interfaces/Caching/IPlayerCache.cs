namespace Chess.Application.Interfaces.Caching;

public interface IPlayerCache
{
    Player Find(int id);
    Player[] FindAll();
    void Add(Player player);
    bool IsInGame(int id);
}