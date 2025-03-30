using FinanceTrackingWebAPI.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinanceTrackingWebAPI.Model
{
    public class GroupVM
    {
        public int Id { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required, DefaultValue(true)]
        public bool IsActive { get; set; }
        public int Count { get; set; }

        public List<string> UserId { get; set; }

    }
}
