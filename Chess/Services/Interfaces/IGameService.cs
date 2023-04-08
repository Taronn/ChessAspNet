using Chess.DTOs;

namespace Chess.Services
{
    public interface IGameService
    {
        GameDto CreateGame(ChallengeDto challenge);
        public GameDto? RemoveGame(PlayerDto player);
        public GameDto? GetGame(PlayerDto player);
        public GameDto? GetGameByUserId(string id);
        public PlayerDto? GetOpponent(PlayerDto player, bool onlineOnly = true);
        public bool IsPlayerTurn(PlayerDto player, GameDto game);
        public MoveResultType MakeMove(PlayerDto player, GameDto game, string from, string to);
        public Task<EndgameType?> EndGameAsync(PlayerDto player);
        public bool Resign(PlayerDto player);
        public bool OfferDraw(PlayerDto player);
        public bool AcceptDraw(PlayerDto player);
        public bool RejectDraw(PlayerDto player);
    }
}
