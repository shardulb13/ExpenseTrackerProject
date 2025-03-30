using FinanceTrackingWebAPI.Authentication;
using System.ComponentModel.DataAnnotations;

namespace FinanceTrackingWebAPI.Entities
{
    public class UsersGroup
    {
        [Key]
        public int Id { get; set; }

        public int? GroupId { get; set; }
        public Group Groups { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
