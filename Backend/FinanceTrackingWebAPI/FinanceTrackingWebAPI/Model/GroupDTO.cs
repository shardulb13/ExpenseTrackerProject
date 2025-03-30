using System;
using System.Collections.Generic;

namespace FinanceTrackingWebAPI.Model
{
    public class GroupDTO
    {
        public int ExpensesId { get; set; }

        public string ExpenseName { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int Amount { get; set; }

        public string PaidBy { get; set; }
        public int GroupId { get; set; }
        public List<string> UserIds { get; set; }
    }
}
