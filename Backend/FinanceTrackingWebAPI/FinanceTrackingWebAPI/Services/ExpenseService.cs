using FinanceTrackingWebAPI.Data;
using FinanceTrackingWebAPI.DataAccessLayer;
using FinanceTrackingWebAPI.Entities;
using FinanceTrackingWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.Services
{
    public interface IExpenseService
    {
        IEnumerable<ExpenseVM> GetAllExpenses(string userId);
        IEnumerable<ExpenseVM> GroupExpenses(int groupId);
        Task<int> AddExpense(ExpenseVM expense);
        Task<int> UpdateExpense(ExpenseVM expense);
        bool DeleteExpense(int id);
    }
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseDA _expenseDA;

        public ExpenseService(IExpenseDA expenseDA)
        {
            _expenseDA = expenseDA;
        }

        public async Task<int> AddExpense(ExpenseVM expense)
        {
            var expenseModel = new Expense
            {
                ExpenseName = expense.ExpenseName,
                ExpenseDate = expense.ExpenseDate,
                Amount = expense.Amount,
                PaidBy = expense.PaidBy,
                GroupId = expense.GroupId,
                CreatedBy = expense.ExpensesId,
                CreatedOn = DateTime.Now,
                IsActive = expense.IsActive,
            };
            var expenseId = await _expenseDA.AddExpense(expenseModel);
            if (expense.UserId.Count == 0)
            {
                return expenseId;
            }
            foreach (var id in expense.UserId)
            {
                var userExpenses = new UserExpenses
                {
                    ExpenseId = expenseModel.ExpensesId,
                    UserId = id,
                };
                await _expenseDA.UserExpenses(userExpenses);
            }
            return expenseId;
        }

        public bool DeleteExpense(int id)
        {
            return _expenseDA.DeleteExpense(id);
        }

        public IEnumerable<ExpenseVM> GetAllExpenses(string userId)
        {
            var result = _expenseDA.GetAllExpenses(userId);
            if (result != null)
            {
                return (from exp in result
                        select new ExpenseVM
                        {
                            ExpensesId = exp.expenseId,
                            ExpenseName = exp.expenseName,
                            ExpenseDate = exp.date,
                            Amount = exp.amount,
                            PaidBy = exp.paidby,
                            UserId = exp.userIds
                        }).ToList();
            }
            return null;
        }
        public IEnumerable<ExpenseVM> GroupExpenses(int groupId)
        {
            var result = _expenseDA.GroupExpenses(groupId);
            if (result != null)
            {
                return (from exp in result
                        select new ExpenseVM
                        {
                            ExpensesId = exp.ExpensesId,
                            ExpenseName = exp.ExpenseName,
                            ExpenseDate = exp.ExpenseDate,
                            Amount = exp.Amount,
                            PaidBy = exp.PaidBy,
                            GroupId = exp.GroupId,
                            UserId = exp.UserIds
                        }).ToList();
            }
            return null;
        }

        public async Task<int> UpdateExpense(ExpenseVM expense)
        {
            var expenseModel = new Expense
            {
                ExpensesId = expense.ExpensesId,
                ExpenseName = expense.ExpenseName,
                ExpenseDate = expense.ExpenseDate,
                Amount = expense.Amount,
                PaidBy = expense.PaidBy,
            };
            return await _expenseDA.UpdateExpense(expenseModel);
        }
    }
}
