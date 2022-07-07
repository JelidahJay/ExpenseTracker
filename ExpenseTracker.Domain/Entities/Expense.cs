using ExpenseTracker.Utilities.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Entities
{
    /// <summary>
    /// Expense entity.
    /// </summary>
    public class Expense : BaseEntity
    {
        /// <summary>
        /// Primary key of the expenses table.
        /// </summary>
        [Key]
        public int ExpenseID { get; set; }

        /// <summary>
        /// Date of expenditure.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Expense Date")]
        public DateTime ExpenseDate { get; set; }

        /// <summary>
        /// Expense amount.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal Amount { get; set; }

        /// <summary>
        /// Foreign key, reference of Categories table.
        /// </summary>
        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }   
    }
}