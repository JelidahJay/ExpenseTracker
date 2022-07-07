namespace ExpenseTracker.Utilities.Constants
{
    /// <summary>
    /// Entity containing all the common routes used in the controllers.
    /// </summary>
    public static class RouteConstants
    {
        /// <summary>
        /// Route : Categories controller.
        /// </summary>
        public const string ExpenseCategoriesController = "api/expense-tracker";

        /// <summary>
        /// Route : Get all categories.
        /// </summary>
        public const string Categories = "categories";

        /// <summary>
        /// Route : Get category by ID.
        /// </summary>
        public const string CategoryByKey = "category/key/";

        /// <summary>
        /// Route : Post - create a new category.
        /// </summary>
        public const string CreateCategory = "category/create";

        /// <summary>
        /// Route : Put - update a category.
        /// </summary>
        public const string UpdateCategory = "category/update/key/";

        /// <summary>
        /// Route : Delete - delete a category.
        /// </summary>
        public const string DeleteCategory = "category/delete/key/";

        /// <summary>
        /// Route : Get all expenses.
        /// </summary>
        public const string Expenses = "expenses";

        /// <summary>
        /// Route : Get expenses by ID.
        /// </summary>
        public const string ExpenseByKey = "expense/key/";

        /// <summary>
        /// Route : Post - create a new expense.
        /// </summary>
        public const string CreateExpense = "expense/create";

        /// <summary>
        /// Route : Put - update an expense.
        /// </summary>
        public const string UpdateExpense = "expense/update/key/";

        /// <summary>
        /// Route : Delete - delete an expense.
        /// </summary>
        public const string DeleteExpense = "expense/delete/key/";
    }
}