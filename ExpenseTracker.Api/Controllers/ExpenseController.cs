using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Controllers
{
    /// <summary>
    /// URL: http://localhost:8800/api/expense-tracker/
    /// </summary>
    [Route(RouteConstants.ExpenseCategoriesController)]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseTrackerContext context;

        public ExpenseController(ExpenseTrackerContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL : http://localhost:8800/api/expense-tracker/expenses/
        /// </summary
        [HttpGet]
        [Route(RouteConstants.Expenses)]
        public async Task<IActionResult> GetAllExpenses()
        {
            try
            {
                var Expenses = await context.Expenses
                    .AsNoTracking()
                    .ToListAsync();

                return Ok(Expenses);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// URL : http://localhost:8800/api/expense-tracker/expense/{id}/
        /// </summary>
        /// <param name="ExpenseID"></param>
        [HttpGet]
        [Route(RouteConstants.ExpenseByKey + "{id}")]
        public async Task<IActionResult> ReadExpenseByKey(int id)
        {

            try
            {
                if (id <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest);

                var expense = await context.Expenses.FindAsync(id);

                if (expense == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                return Ok(expense);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// URL : http://localhost:8800/api/expense-tracker/expense/create/
        /// </summary>
        [HttpPost]
        [Route(RouteConstants.CreateExpense)]
        public async Task<IActionResult> CreateExpense(Expense expense)
        {
            try
            {

                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (await IsAmountLessOrNot(expense.Amount))
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (await IsExpenseDatePastOrFuture(expense.ExpenseDate))
                    return StatusCode(StatusCodes.Status400BadRequest);

                context.Expenses.Add(expense);
                await context.SaveChangesAsync();

                return CreatedAtAction("CreateExpense", new { id = expense.ExpenseID }, expense);
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        } 

        /// <summary>
        /// URL : http://localhost:8800/api/expense-tracker/update-expense/
        /// </summary>
        [HttpPut]
        [Route(RouteConstants.UpdateExpense + "{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Expense expense)
        {
            try
            {
                if (id <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (id != expense.ExpenseID)
                    return StatusCode(StatusCodes.Status404NotFound);

                if (await IsAmountLessOrNot(expense.Amount))
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (await IsExpenseDatePastOrFuture(expense.ExpenseDate))
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest);

                context.Entry(expense).State = EntityState.Modified;
                context.Expenses.Update(expense);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete expense using primary key ExpenseID.
        /// </summary>
        /// <param name="int id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteExpense + "{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                if (id <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest);

                var expense = await context.Expenses.FindAsync(id);

                if (expense == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                context.Expenses.Remove(expense);
                await context.SaveChangesAsync();

                return Ok(expense);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Check whether the ExpenseDate supplied by the user is not future date.
        /// </summary>
        /// <param name="ExpenseDate"></param>
        /// <returns></returns>
        private async Task<bool> IsExpenseDatePastOrFuture(DateTime ExpenseDate)
        {
            try
            {
                var date = DateTime.Today;
                if (ExpenseDate.Date > date.Date)
                    return true;
                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Check whether the Amount is less than or equal to zero.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private async Task<bool> IsAmountLessOrNot(decimal amount)
        {
            try
            {
                if (amount <= 0)
                    return true;
                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}