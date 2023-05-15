using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Validations
{
    public class IntGreaterThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior que zero.");
            }

            if (!int.TryParse(value.ToString(), out int convertedValue))
            {
                return new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior que zero.");
            }

            return convertedValue <= 0
                ? new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior que zero.")
                : ValidationResult.Success;
        }
    }
    public class IntGreaterOrEqualThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior ou igual a zero.");
            }

            if (!int.TryParse(value.ToString(), out int convertedValue))
            {
                return new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior ou igual a zero.");
            }

            return convertedValue < 0
                ? new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior ou igual a zero.")
                : ValidationResult.Success;
        }
    }
    public class DecimalGreaterThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior que zero.");
            }

            if (!decimal.TryParse(value.ToString(), out decimal convertedValue))
            {
                return new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior que zero.");
            }

            return convertedValue <= 0
                ? new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior que zero.")
                : ValidationResult.Success;
        }
    }
    public class DecimalGreaterOrEqualThanZeroAttibute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior ou igual a zero.");
            }

            if (!decimal.TryParse(value.ToString(), out decimal convertedValue))
            {
                return new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior ou igual a zero.");
            }

            return convertedValue < 0
                ? new ValidationResult(errorMessage: validationContext.DisplayName + " deve ser maior ou igual a zero.")
                : ValidationResult.Success;
        }
    }
}
