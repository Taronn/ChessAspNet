using Chess.Domain.Enums;

namespace Chess.Infrastructure.Repositories
{
    internal class FriendsRepository : IFriendsRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public FriendsRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<FriendRequests> GetRequest(Guid id)
        {
            await using var connection = _sqlConnectionFactory.Create();
            var query = $"SELECT * FROM \"{nameof(FriendRequests)}\" " +
                        $"WHERE \"{nameof(FriendRequests.Id)}\" = @id";
            var param = new
            {
                id = id
            };
            var res = await connection.QueryFirstOrDefaultAsync<FriendRequests>(query, param);
            return res;
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
                receiverId = receiverId
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

        public async Task<FriendRequests> UpdateRequest(Guid requestId, FriendRequestStatus status)
        {
            await using var connection = _sqlConnectionFactory.Create();
            var query = $"UPDATE \"{nameof(FriendRequests)}\" " +
                        $"SET \"{nameof(FriendRequests.Status)}\" = @status " +
                        $"WHERE \"{nameof(FriendRequests.Id)}\" = @requestId ";
            var param = new
            {
                status = status,
                requestId = requestId
            };

            var res = await connection.QueryAsync<FriendRequests>(query, param);
            var first = res.FirstOrDefault();
            return first;
        }
    }
}
