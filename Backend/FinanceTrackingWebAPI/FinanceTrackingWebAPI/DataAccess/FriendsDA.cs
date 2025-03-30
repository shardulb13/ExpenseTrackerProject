using FinanceTrackingWebAPI.Authentication;
using FinanceTrackingWebAPI.Data;
using FinanceTrackingWebAPI.Entities;
using FinanceTrackingWebAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.DataAccessLayer
{
    public interface IFriendsDA
    {
        IEnumerable<Friend> GetFriends(string userId);
        IEnumerable<FriendsDTO> GetFriendsData(string userId);
        bool AddFriend(List<Friend> friends);
        bool DeleteFriend(string friendId);

    }
    public class FriendsDA : IFriendsDA
    {
        private readonly ApplicationDbContext _context;
        public FriendsDA(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool DeleteFriend(string friendUserId)
        {
            var result = _context.Friends.Where(a => a.applicationUser.UserName == friendUserId || a.ApplicationUser.UserName == friendUserId);
            if (result != null)
            {
                _context.Friends.RemoveRange(result);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddFriend(List<Friend> friends)
        {
            var singleFriend = friends.First();
            var friendAlreadyExists = _context.Friends.Where(x => x.FriendUserId == singleFriend.FriendUserId && x.UserId == singleFriend.UserId || x.UserId == singleFriend.FriendUserId && x.FriendUserId == singleFriend.UserId).Count();
            if (friendAlreadyExists == 2)
            {
                return false;
            }
            else
            {
                _context.Friends.AddRange(friends);
                _context.SaveChanges();
                return true;
            }
        }

        public IEnumerable<Friend> GetFriends(string userId)
        {
            return _context.Friends.Where(x => x.UserId == userId).Include(x => x.applicationUser).ToList();

        }

        public IEnumerable<FriendsDTO> GetFriendsData(string userId)
        {
            return _context.Friends.Join(_context.Users,
                                 friends => friends.FriendUserId,
                                 users => users.Id,
                                 (friends, users) => new FriendsDTO
                                 {
                                     UserId = friends.UserId,
                                     FriendUserId = friends.FriendUserId,
                                     FriendName = friends.applicationUser.FirstName + " " + friends.applicationUser.LastName,
                                     UserName = friends.applicationUser.UserName,
                                 }).Where(x => x.UserId == userId).ToList();
        }
    }
}
