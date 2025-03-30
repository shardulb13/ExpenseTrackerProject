using FinanceTrackingWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceTrackingWebAPI.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePhoto { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<UserExpenses> userExpenses { get; set; }

        public List<UsersGroup> usersGroup { get; set; }

        public ICollection<Friend> Friend { get; set; }



    }
}
