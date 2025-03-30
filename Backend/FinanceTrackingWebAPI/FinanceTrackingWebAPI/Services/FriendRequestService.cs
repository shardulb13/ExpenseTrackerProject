using FinanceTrackingWebAPI.DataAccess;
using FinanceTrackingWebAPI.DataAccessLayer;
using FinanceTrackingWebAPI.Entities;
using FinanceTrackingWebAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace FinanceTrackingWebAPI.Services
{
    public interface IFriendRequestService
    {
        IEnumerable<FriendVM> GetAllFriendRequests(string userId);
        IEnumerable<FriendVM> GetFriendsData(string userId);
        bool AddFriendRequest(FriendVM friends);
        bool DeleteFriendRequest(string friendUserId);

    }
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IFriendRequestDA _friendsRequestDA;
        private readonly IFriendsDA _friendsDA;
        public FriendRequestService(IFriendRequestDA friendRequestDA, IFriendsDA friendsDA)
        {
            _friendsRequestDA = friendRequestDA;

        }

        public bool DeleteFriendRequest(string friendUserId)
        {
            return _friendsRequestDA.DeleteFriendRequest(friendUserId);
        }

        public bool AddFriendRequest(FriendVM friends)
        {
            foreach (var friendUserId in friends.FriendUserId)
            {
                List<FriendRequest> friendsList = new List<FriendRequest>();
                var friendsModel = new FriendRequest
                {
                    UserId = friends.UserId,
                    FriendId = friendUserId,
                    SentBy = friends.UserId
                };
                friendsList.Add(friendsModel);
                var friendsModelForFriend = new FriendRequest
                {
                    UserId = friendUserId,
                    FriendId = friends.UserId,
                    SentBy = friends.UserId
                };
                friendsList.Add(friendsModelForFriend);
                _friendsRequestDA.AddFriendRequest(friendsList);
            }
            return true;
        }

        public IEnumerable<FriendVM> GetAllFriendRequests(string userId)
        {
            var result = _friendsRequestDA.GetFriendRequests(userId).GroupBy(i => i.UserId);
            if (result != null)
            {
                return (from exp in result
                        select new FriendVM
                        {
                            UserId = exp.Key,
                            FriendUserId = exp.Select(x => x.FriendId).ToList()
                        });
            }
            return null;
        }

        public IEnumerable<FriendVM> GetFriendsData(string userId)
        {
            var result = _friendsRequestDA.GetFriendsData(userId);
            var list = new List<string>();
            if (result != null)
            {
                list.Add(result.Select(x => x.FriendUserId).FirstOrDefault());
                return (from exp in result
                        select new FriendVM
                        {
                            UserId = exp.UserId,
                            FriendUserId = list,
                            SingleFriendUserId = exp.FriendUserId,
                            Friendname = exp.FriendName,
                            Username = exp.UserName,
                            Sentby = exp.SentBy
                        });
            }
            
            return null;
        }
    }
}

