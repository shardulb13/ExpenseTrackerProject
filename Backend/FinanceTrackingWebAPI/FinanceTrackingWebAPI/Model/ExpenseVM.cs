using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTrackingWebAPI.Model
{
    public class ExpenseVM
    {
        public int ExpensesId { get; set; }

        [Required]
        public string ExpenseName { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string PaidBy { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required, DefaultValue(true)]
        public bool IsActive { get; set; }

        public int? GroupId { get; set; }

        public List<string> UserId { get; set; }
    }
}
