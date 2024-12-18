using static Chess.Application.Services.GameService;

namespace Chess.Application.Interfaces.Services;

public interface IGameService
{
    Game Create(Invite invite);
    Game Find(Guid id);
    void Remove(Game game);
    Player GetOpponent(Guid id, bool onlineOnly = true);
    Task<EndgameType?> EndGameAsync(Player player);
    bool IsPlayerTurn(Player player, Game game);
    bool Resign(Player player);
    bool OfferDraw(Player player);
    bool AcceptDraw(Player player);
    bool RejectDraw(Player player);
    MoveResultType MakeMove(Player player, Game game, string from, string to);
}