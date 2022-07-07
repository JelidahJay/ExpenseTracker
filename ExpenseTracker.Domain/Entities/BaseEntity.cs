using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Entities
{
    /// <summary>
    /// Base properties of each class including datetime and date modified.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Creation date of the row.
        /// </summary>
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Last modifiation of the date of the row.
        /// </summary>
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateModified { get; set; }
    }
}