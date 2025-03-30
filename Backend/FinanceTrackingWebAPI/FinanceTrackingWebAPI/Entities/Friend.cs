using FinanceTrackingWebAPI.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTrackingWebAPI.Entities
{
    public class Friend
    {

        [Key]
        public int FriendId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("applicationUser")]
        public string FriendUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

    }
}
