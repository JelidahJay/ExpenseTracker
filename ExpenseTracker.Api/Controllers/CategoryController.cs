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
    public class CategoryController : ControllerBase
    {
        private readonly ExpenseTrackerContext context;

        public CategoryController(ExpenseTrackerContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// URL : http://localhost:8800/api/expense-category/categories/
        /// </summary
        [HttpGet]
        [Route(RouteConstants.Categories)]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await context.Categories
                    .AsNoTracking()
                    .OrderBy(c => c.CategoryName)
                    .ToListAsync();

                return Ok(categories);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// URL : http://localhost:8800/api/expense-category/category/{id}/
        /// </summary>
        /// <param name="CategoryID"></param>
        [HttpGet]
        [Route(RouteConstants.CategoryByKey + "{id}")]
        public async Task<IActionResult> GetCategoryByKey(int id)
        {

            try
            {
                if (id <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest);

                var category = await context.Categories
                              .Include(p => p.Expenses)
                              .Where(p => p.CategoryID == id)
                              .FirstOrDefaultAsync();

                if (category == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                return Ok(category);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// URL : http://localhost:8800/api/expense-category/category/create/
        /// </summary>
        [HttpPost]
        [Route(RouteConstants.CreateCategory)]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (await IsCategoryDuplicate(category))
                    return StatusCode(StatusCodes.Status400BadRequest);

                context.Categories.Add(category);
                await context.SaveChangesAsync();

                return CreatedAtAction("CreateCategory", new { id = category.CategoryID }, category);
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// URL : http://localhost:8800/api/expense-category/update-category/
        /// </summary>
        [HttpPut]
        [Route(RouteConstants.UpdateCategory + "{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            try
            {
                if (id <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (id != category.CategoryID)
                    return StatusCode(StatusCodes.Status404NotFound);

                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (await IsCategoryDuplicate(category))
                    return StatusCode(StatusCodes.Status400BadRequest);

                context.Entry(category).State = EntityState.Modified;
                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// URL : http://localhost:8800/api/expense-category/delete/key
        /// </summary>
        [HttpDelete]
        [Route(RouteConstants.DeleteCategory + "{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                if (id <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest);

                var category = await context.Categories.FindAsync(id);

                if (category == null)
                    return StatusCode(StatusCodes.Status400BadRequest);

                if (await IsCategoryInUse(category))
                    return StatusCode(StatusCodes.Status400BadRequest);

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Verifying the category name is duplicate or not.
        /// </summary>
        /// <param name="category">Category object.</param>
        /// <returns>Boolean</returns>
        private async Task<bool> IsCategoryDuplicate(Category category)
        {
            try
            {
                var categoryInDb = await context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == category.CategoryName.ToLower());

                if (categoryInDb != null)
                    return true;

                return false;
            }
            catch
            {
                throw;
            }
        }

        // <summary>
        /// Verifying the category is in use or not.
        /// </summary>
        /// <param name = "category">Category object.</param>
        /// <returns>Bool</returns>
        private async Task<bool> IsCategoryInUse(Category category)
        {
            try
            {
                var expense = await context.Expenses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.CategoryID == category.CategoryID);

                if (expense != null)
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