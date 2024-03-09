namespace Chess.Application.Interfaces.Repositories
{
    public interface IFriendsRepository
    {
        Task<FriendRequests> SendFriendRequest(int senderId, int recieverId);
        Task<Friends> GetFriends(int userId1, int userId2);
    }
}
