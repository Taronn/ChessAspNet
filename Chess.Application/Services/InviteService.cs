using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;
using Chess.Domain.Entities;
using System;

namespace Chess.Application.Services
{
    public class InviteService : IInviteService
    {
        private readonly IInviteCache _cache;
        private readonly IPlayerCache _player;

        public InviteService(IInviteCache cache, IPlayerCache player)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public void SendInvitation(int playerId1, int playerId2, GameConfiguration gc, DateTime time)
        {
            if (gc == null)
                throw new ArgumentNullException(nameof(gc));

            // Perform parameter validation if needed

            var player1 = _player.Find(playerId1);
            var player2 = _player.Find(playerId2);

            if (player1 == null || player2 == null)
                throw new ArgumentException("Invalid player ID");

            var invite = new Invite
            {
                From = player1,
                To = player2,
                FromId = playerId1,
                ToId = playerId2,
                GameConfiguration = gc,
                CreatedAt = time
            };

            _cache.Add(invite);
        }
    }
}
