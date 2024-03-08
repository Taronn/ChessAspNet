using System.Collections.Concurrent;
using Chess.Application.Interfaces.Caching;

namespace Chess.Infrastructure.Caching;

public class PlayerCache : IPlayerCache
{
    private readonly ConcurrentDictionary<int, Player> _players = new ConcurrentDictionary<int, Player>(
        new[]
        {
        new KeyValuePair<int, Player>(1, new Player { Id = 1, Username = "Player1" }),
        new KeyValuePair<int, Player>(2, new Player { Id = 2, Username = "Player2" }),
        }
    );


    public Player Find(int id)
    {
        return _players.GetValueOrDefault(id);
    }

    public Player[] FindAll()
    {
        return _players.Values.ToArray();
    }

    public void Add(Player player)
    {
        _players[player.Id] = player;
    }
}