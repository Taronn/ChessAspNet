using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;

namespace Chess.Application.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IFriendsRepository _friendsRepository;
        private readonly IUserRepository _userRepository;
        public FriendsService(IFriendsRepository friendsRepository, IUserRepository userRepository)
        {
            this._friendsRepository = friendsRepository;
            this._userRepository = userRepository;
        }
        public async Task<bool> SendFriendRequest(int senderId, int recieverId)
        {
            if(senderId == recieverId)
            {
                throw new Exception("ERROR");
            }

            var user = await _userRepository.FindAsync(recieverId);
            if (user == null)
            {
                throw new Exception("User is not exists");
            }

            var friends = await _friendsRepository.GetFriends(senderId, recieverId);
            if(friends != null)
            {
                throw new Exception("It is not possible to send a friend request more than once.");
            }

            var res = await _friendsRepository.SendFriendRequest(senderId, recieverId);
            if(res == null)
            {
                return false;
            }

            return true;
        }
    }
}
