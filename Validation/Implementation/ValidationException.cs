using System;

namespace Validation.Implementation
{
    public class ValidationException : ArgumentException
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}