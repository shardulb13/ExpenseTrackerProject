using System.Collections.Generic;

namespace FinanceTrackingWebAPI.Model
{
    public class FriendVM
    {
        public int FriendId { get; set; }
        public string UserId { get; set; }
        public string Friendname { get; set; }
        public List<string> FriendUserId { get; set; }
        public string SingleFriendUserId { get; set; }
        public string Username { get; set; }
    }
}
