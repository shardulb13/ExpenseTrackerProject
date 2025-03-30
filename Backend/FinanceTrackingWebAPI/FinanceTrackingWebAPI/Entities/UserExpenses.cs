using FinanceTrackingWebAPI.Authentication;
using System;

namespace FinanceTrackingWebAPI.Entities
{
    public class UserExpenses
    {
        public int Id { get; set; }

        public int? ExpenseId { get; set; }
        public Expense Expenses { get; set;}

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
