using FinanceTrackingWebAPI.Data;
using FinanceTrackingWebAPI.Entities;
using FinanceTrackingWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.DataAccessLayer
{
    public interface IExpenseDA
    {
        IEnumerable<UserExpenseDTO> GetAllExpenses(string userid);
        IEnumerable<GroupDTO> GroupExpenses(int groupId);
        Task<int> AddExpense(Expense expense);
        Task<UserExpenses> UserExpenses(UserExpenses userExpense);
        Task<int> UpdateExpense(Expense expenses);
        bool DeleteExpense(int id);

    }
    public class ExpenseDA : IExpenseDA
    {
        private readonly ApplicationDbContext _context;
        public ExpenseDA(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddExpense(Expense expenses)
        {
            await _context.Expenses.AddAsync(expenses);
            _context.SaveChanges();
            return expenses.ExpensesId;
        }
        public async Task<UserExpenses> UserExpenses(UserExpenses userExpense)
        {
            var result = await _context.UserExpenses.AddAsync(userExpense);
            _context.SaveChanges();
            return result.Entity;
        }

        public bool DeleteExpense(int id)
        {
            var result = _context.Expenses.FirstOrDefault(a => a.ExpensesId == id);
            if (result != null)
            {
                _context.Expenses.Remove(result);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<GroupDTO> GroupExpenses(int groupId)
        {

            return _context.Expenses.Where(a => a.GroupId == groupId).Select(o => new GroupDTO
            {
                ExpensesId = o.ExpensesId,
                ExpenseName = o.ExpenseName,
                ExpenseDate = o.ExpenseDate,
                Amount = o.Amount,
                PaidBy = o.ApplicationUser.UserName,
                GroupId = o.Groups.Id,
                UserIds = o.Groups.usersGroup.Select(ui => ui.ApplicationUser.UserName).ToList()
            });
        }
        public IEnumerable<UserExpenseDTO> GetAllExpenses(string userid)
        {
            return _context.Expenses.Select(o => new UserExpenseDTO
            {
                expenseId = o.ExpensesId,
                expenseName = o.ExpenseName,
                amount = o.Amount,
                date = o.ExpenseDate,
                paidby = o.ApplicationUser.UserName,
                userIds = o.userExpenses.Select(un => un.ApplicationUser.UserName).ToList()

            }).Where(a => a.userIds.Contains(userid)).ToList();
        }

        public async Task<int> UpdateExpense(Expense expenses)
        {
            var update = await _context.Expenses.FirstOrDefaultAsync(a => a.ExpensesId == expenses.ExpensesId);
            if (update != null)
            {
                update.ExpenseName = expenses.ExpenseName;
                update.ExpenseDate = expenses.ExpenseDate;
                update.Amount = expenses.Amount;
                update.PaidBy = expenses.PaidBy;
                await _context.SaveChangesAsync();
                return expenses.ExpensesId;
            }
            throw new System.Exception();
        }
    }
}
