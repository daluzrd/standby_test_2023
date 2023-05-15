using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Validations;

public class DateGreaterThanTodayAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult(validationContext.DisplayName + " está inválido.");

        if (!DateTime.TryParse(value.ToString(), out DateTime valor))
            return new ValidationResult(validationContext.DisplayName + " está inválido.");

        return valor.Ticks < DateTime.Now.Ticks
            ? new ValidationResult($"{validationContext.DisplayName} deve ser maior que {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.")
            : ValidationResult.Success;
    }
}
