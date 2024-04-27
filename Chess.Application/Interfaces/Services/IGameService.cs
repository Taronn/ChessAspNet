using static Chess.Application.Services.GameService;

namespace Chess.Application.Interfaces.Services;

public interface IGameService
{
    Game Create(Invite invite);
    Game Find(Guid id);
    void Remove(Guid id);
    Player GetOpponent(Player player, bool onlineOnly = true);
/*    async Task<EndgameType?> EndGameAsync(Player player);
*/    bool IsPlayerTurn(Player player, Game game);
    bool Resign(Player player);
    bool OfferDraw(Player player);
/*    async Task UpdateStats(Game game);
*/    bool AcceptDraw(Player player);
    bool RejectDraw(Player player);
    MoveResultType MakeMove(Player player, Game game, string from, string to);
}