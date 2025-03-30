using FinanceTrackingWebAPI.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTrackingWebAPI.Entities
{
    public class Expense
    {
        [Key]
        public int ExpensesId { get; set; }

        [Required, MaxLength(20)]
        public string ExpenseName { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]

        [ForeignKey("ApplicationUser")]
        public string PaidBy { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [Required, DefaultValue(true)]
        public bool IsActive { get; set; }

        [ForeignKey("Groups")]
        public int? GroupId { get; set; }
        public Group Groups { get; set; }

        //public List<UsersGroup> usersGroups { get; set; }

        public List<UserExpenses> userExpenses { get; set; }


    }
}
