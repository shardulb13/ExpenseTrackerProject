using FinanceTrackingWebAPI.Authentication;
using FinanceTrackingWebAPI.DataAccessLayer;
using FinanceTrackingWebAPI.Entities;
using FinanceTrackingWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.Services
{
    public interface IFriendsService
    {
        IEnumerable<FriendVM> GetAllFriends(string userId);
        IEnumerable<FriendVM> GetFriendsData(string userId);
        bool AddFriend(FriendVM friends);
        bool DeleteFriend(string friendUserId);

    }
    public class FriendService : IFriendsService
    {
        private readonly IFriendsDA _friendsDA;
        public FriendService(IFriendsDA friendsDA)
        {
            _friendsDA = friendsDA;
        }

        public bool DeleteFriend(string friendUserId)
        {
            return _friendsDA.DeleteFriend(friendUserId);
        }

        public bool AddFriend(FriendVM friends)
        {
            foreach (var friendUserId in friends.FriendUserId)
            {
                List<Friend> friendsList = new List<Friend>();
                var friendsModel = new Friend
                {
                    UserId = friends.UserId,
                    FriendUserId = friendUserId,
                };
                friendsList.Add(friendsModel);
                var friendsModelForFriend = new Friend
                {
                    UserId = friendUserId,
                    FriendUserId = friends.UserId
                };
                friendsList.Add(friendsModelForFriend);
                _friendsDA.AddFriend(friendsList);
            }
            return true;
        }

        public IEnumerable<FriendVM> GetAllFriends(string userId)
        {
            var result = _friendsDA.GetFriends(userId).GroupBy(i => i.UserId);
            if (result != null)
            {
                return (from exp in result
                        select new FriendVM
                        {
                            UserId = exp.Key,
                            FriendUserId = exp.Select(x => x.applicationUser.UserName).ToList()
                        });
            }
            return null;
        }

        public IEnumerable<FriendVM> GetFriendsData(string userId)
        {
            var result = _friendsDA.GetFriendsData(userId);
            if (result != null)
            {
                return (from exp in result
                        select new FriendVM
                        {
                            UserId = exp.UserId,
                            SingleFriendUserId = exp.FriendUserId,
                            Friendname = exp.FriendName,
                            Username = exp.UserName
                        });
            }
            return null;
        }
    }
}
