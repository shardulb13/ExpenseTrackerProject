using FinanceTrackingWebAPI.Authentication;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinanceTrackingWebAPI.Entities
{
    public class FriendRequest
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }  

        public string FriendId { get; set; }

        public string SentBy { get; set; }
    }
}
