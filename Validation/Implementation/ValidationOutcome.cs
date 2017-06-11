namespace Validation.Implementation
{
    public class ValidationOutcome
    {
        public bool Valid { get; }
        public string Message { get; }

        public ValidationOutcome()
        {
            Valid = true;
        }

        public ValidationOutcome(string message)
        {
            Message = message;
            Valid = false;
        }
    }
}