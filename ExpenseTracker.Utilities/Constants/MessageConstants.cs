using System;
namespace ExpenseTracker.Utilities.Constants
{
    /// <summary>
    /// Entity containing common constants commonly used by other entities.
    /// </summary>
    public static class MessageConstants
    {
        /// <summary>
        /// Message to display that a field is required.
        /// </summary>
        public const string RequiredError = "Required!";

        /// <summary>
        /// Error message to show thhe maximum length of field has been reached.
        /// </summary>
        public const string LengthError = "Maximum length reached!";
    }
}