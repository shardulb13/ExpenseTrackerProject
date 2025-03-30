using FinanceTrackingWebAPI.Data;
using FinanceTrackingWebAPI.Entities;
using FinanceTrackingWebAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace FinanceTrackingWebAPI.DataAccess
{
    public interface IFriendRequestDA
    {
        IEnumerable<FriendRequest> GetFriendRequests(string userId);
        IEnumerable<FriendsDTO> GetFriendsData(string userId);
        bool AddFriendRequest(List<FriendRequest> friends);
        bool DeleteFriendRequest(string friendId);

    }

    public class FriendRequestDA : IFriendRequestDA
    {
        private readonly ApplicationDbContext _context;
        public FriendRequestDA(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool DeleteFriendRequest(string friendUserId)
        {
            var result = _context.FriendRequests.Where(a => a.UserId == friendUserId || a.FriendId == friendUserId);
            if (result != null)
            {
                _context.FriendRequests.RemoveRange(result);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddFriendRequest(List<FriendRequest> friends)
        {
            var singleFriend = friends.First();
            var friendAlreadyExists = _context.FriendRequests.Where(x => x.FriendId == singleFriend.FriendId
            && x.UserId == singleFriend.UserId || x.UserId == singleFriend.FriendId && x.FriendId == singleFriend.UserId).Count();
            if (friendAlreadyExists == 2)
            {
                return false;
            }
            else
            {
                _context.FriendRequests.AddRange(friends);
                _context.SaveChanges();
                return true;
            }
        }

        public IEnumerable<FriendRequest> GetFriendRequests(string userId)
        {
            return _context.FriendRequests.Where(x => x.UserId == userId).ToList();

        }

        public IEnumerable<FriendsDTO> GetFriendsData(string userId)
        {
            return _context.FriendRequests.Join(_context.Users,
                                 friends => friends.FriendId,
                                 users => users.Id,
                                 (friends, users) => new FriendsDTO
                                 {
                                     UserId = friends.UserId,
                                     FriendUserId = friends.FriendId,
                                     FriendName = users.FirstName + " " + users.LastName,
                                     UserName = users.UserName,
                                     SentBy = friends.SentBy
                                     
                                 }).Where(x => x.UserId == userId).ToList();
        }
    }
}

