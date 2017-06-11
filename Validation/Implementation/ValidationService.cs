using System;

namespace Validation.Implementation
{
    public class ValidationService
    {
        public static void ThrowOnInvalid<TValue>(Func<TValue, ValidationOutcome> validator, TValue value)
        {
            ValidationOutcome vo = validator(value);
            if (!vo.Valid)
            {
                throw new ValidationException(vo.Message);
            }
        }

        public static ValidationOutcome ValidateStringMinLength(string value, int minLength, string message)
        {
            return Validate<string>(value, (v => v.Length >= minLength), message);
        }

        public static ValidationOutcome ValidateStringMaxLength(string value, int maxLength, string message)
        {
            return Validate<string>(value, (v => v.Length <= maxLength), message);
        }

        public static ValidationOutcome ValidateStringContains(string value, string containedString, string message)
        {
            return Validate<string>(value, v => v.Contains(containedString), message);
        }

        public static ValidationOutcome ValidateStringContainsNot(string value, string containedString, string message)
        {
            return Validate<string>(value, v => !v.Contains(containedString), message);
        }

        public static ValidationOutcome ValidateIntInInterval(int value, int minValue, int MaxValue, string message)
        {
            return Validate<int>(value, v => (v >= minValue && v <= MaxValue), message);
        }

        private static ValidationOutcome Validate<TValue>(TValue value, Func<TValue, bool> isValid, string message)
        {
            return isValid(value) ? new ValidationOutcome() : new ValidationOutcome(message);
        }
    }
}