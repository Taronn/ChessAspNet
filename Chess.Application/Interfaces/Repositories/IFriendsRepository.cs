namespace Chess.Application.Interfaces.Repositories
{
    public interface IFriendsRepository
    {
        Task<FriendRequests> SaveRequest(int senderId, int recieverId);
        Task<bool> IsRequestSent(int senderId, int recieverId);
        Task<bool> IsFriends(int userId1, int userId2);
    }
}
