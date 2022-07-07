using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure
{/// <summary>
/// Data Context Class that interacts with the database.
/// </summary>
    public class ExpenseTrackerContext : DbContext
    {
        /// <summary>
        /// DbSet to represent entities from the Category model.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// DbSet to represent entities from the Expense model.
        /// </summary>
        public DbSet<Expense> Expenses { get; set; }

        /// <summary>
        /// Create a connection string to connect SQL server.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial " +
                                      "Catalog=ExpenseTracker;" +
                                      "User Id=jelida;Password=Jelidah1998#;trusted_connection = true";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}