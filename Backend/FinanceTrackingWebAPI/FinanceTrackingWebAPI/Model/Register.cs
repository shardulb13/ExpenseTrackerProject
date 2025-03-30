using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FinanceTrackingWebAPI.Model
{
    public class Register
    {
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile File { get; set; }

    }
}
