namespace Chess.Application.Interfaces.Repositories
{
    public interface IFriendsRepository
    {
        Task<FriendRequests> SaveRequest(Guid senderId, Guid receiverId);
        Task<bool> IsRequestSent(Guid senderId, Guid receiverId);
        Task<bool> IsFriends(Guid userId1, Guid userId2);
    }
}
