using FinanceTrackingWebAPI.Model;
using FinanceTrackingWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public IActionResult Expenses()
        {
            string userId = User.FindFirstValue(ClaimTypes.Name);
            return Ok(_expenseService.GetAllExpenses(userId));
        }

        [HttpGet]
        [Route("GroupExpenses/{groupId}")]
        public IActionResult Expenses(int groupId)
        {
            return Ok(_expenseService.GroupExpenses(groupId));
        }

        [HttpPost]
        public async Task<IActionResult> Expense(ExpenseVM expense)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    return Ok(await _expenseService.AddExpense(expense));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Expense Creation Failed" });
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpense(ExpenseVM expense)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _expenseService.UpdateExpense(expense));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Id not found" });
            }

        }

        [HttpDelete("{id}")]
        public bool DeleteExpense(int id)
        {
            return _expenseService.DeleteExpense(id);
        }
    }
}
