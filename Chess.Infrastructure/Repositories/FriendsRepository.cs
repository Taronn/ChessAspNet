namespace Chess.Infrastructure.Repositories
{
    internal class FriendsRepository : IFriendsRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public FriendsRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<bool> IsFriends(Guid userId1, Guid userId2)
        {
            var query = $"SELECT COUNT(*) FROM \"{nameof(Friends)}\" " +
                        $"WHERE (\"{nameof(Friends.UserId1)}\" = @userId1 " +
                        $"AND \"{nameof(Friends.UserId2)}\" = @userId2) " +
                        $"OR (\"{nameof(Friends.UserId1)}\" = @userId2 " +
                        $"AND \"{nameof(Friends.UserId2)}\" = @userId1)";
            var param = new
            {
                userId1 = userId1,
                userId2 = userId2
            };

            await using var connection = _sqlConnectionFactory.Create();
            var count = await connection.ExecuteScalarAsync<int>(query, param);

            return count > 0;
        }
        public async Task<bool> IsRequestSent(Guid senderId, Guid receiverId)
        {
            await using var connection = _sqlConnectionFactory.Create();
            var query = $"SELECT COUNT(*) FROM \"{nameof(FriendRequests)}\" " +
                        $"WHERE (\"{nameof(FriendRequests.SenderId)}\" = @senderId " +
                        $"AND \"{nameof(FriendRequests.ReceiverId)}\" = @receiverId) " +
                        $"OR (\"{nameof(FriendRequests.SenderId)}\" = @receiverId " +
                        $"AND \"{nameof(FriendRequests.ReceiverId)}\" = @senderId)";
            var param = new
            {
                senderId = senderId,
                recieverId = receiverId
            };

            var count = await connection.ExecuteScalarAsync<int>(query, param);
            return count > 0;
        }

        public async Task<FriendRequests> SaveRequest(Guid senderId, Guid receiverId)
        {
            await using var connection = _sqlConnectionFactory.Create();
            var query = $"INSERT INTO \"{nameof(FriendRequests)}\" " +
                        $"(\"{nameof(FriendRequests.SenderId)}\", \"{nameof(FriendRequests.ReceiverId)}\") " +
                        $"VALUES (@senderId, @receiverId) " +
                        $"RETURNING *";
            var param = new
            {
                senderId = senderId,
                receiverId = receiverId
            };

            var res = await connection.QueryAsync<FriendRequests>(query, param);
            var first = res.FirstOrDefault();
            return first;
        }
    }
}
