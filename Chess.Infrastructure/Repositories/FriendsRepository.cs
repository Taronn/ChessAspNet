namespace Chess.Infrastructure.Repositories
{
    internal class FriendsRepository : IFriendsRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public FriendsRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Friends> GetFriends(int userId1, int userId2)
        {
            var query = $"SELECT * FROM \"{nameof(Friends)}\" " +
                        $"WHERE \"{nameof(Friends.UserId1)}\" = @userId1 " +
                        $"AND \"{nameof(Friends.UserId2)}\" = @userId2";
            var param = new
            {
                userId1 = userId1,
                userId2 = userId2
            };

            await using var connection = _sqlConnectionFactory.Create();
            var friends = connection.Query<Friends>(query, param).FirstOrDefault();

            return friends;
        }

        public async Task<FriendRequests> SendFriendRequest(int senderId, int recieverId)
        {
            await using(var connection = _sqlConnectionFactory.Create())
            {
                var query = $"INSERT INTO \"{nameof(FriendRequests)}\" " +
                        $"(\"{nameof(FriendRequests.SenderId)}\", \"{nameof(FriendRequests.ReceiverId)}\") " +
                        $"VALUES (@senderId, @recieverId)" +
                        $"RETURNING *";
                var param = new
                {
                    senderId = senderId,
                    recieverId = recieverId
                };

                var res = connection.QueryAsync<FriendRequests>(query, param);
                var frist = res.Result.FirstOrDefault();
                return frist;
            }
        }
    }
}
