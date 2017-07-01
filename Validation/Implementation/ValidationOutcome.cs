namespace Validation.Implementation
{
    /// <summary>
    /// Result of a validation operation
    /// </summary>
    public class ValidationOutcome
    {
        #region Properties
        /// <summary>
        /// Returns true if no errors were found, otherwise false.
        /// </summary>
        public bool Valid { get; }

        /// <summary>
        /// Message containing validation details.
        /// </summary>
        public string Message { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Use this constructor is the validation did not find any errors.
        /// </summary>
        public ValidationOutcome()
        {
            Valid = true;
        }

        /// <summary>
        /// Use this constructor is the validation found errors.
        /// </summary>
        /// <param name="message">
        /// Message detailing the validation error.
        /// </param>
        public ValidationOutcome(string message)
        {
            Message = message;
            Valid = false;
        } 
        #endregion
    }
}