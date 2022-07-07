using ExpenseTracker.Utilities.Constants;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Domain.Entities
{
    /// <summary>
    /// Category entity.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Primary key for the table Categories table.
        /// </summary>
        [Key]
        public int CategoryID { get; set; }

        /// <summary>
        /// Name of the category for each expense.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [StringLength(60)]
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Name of the expense category.
        /// </summary>
        public virtual List<Expense> Expenses { get; set; } = new List<Expense>();
    }
}