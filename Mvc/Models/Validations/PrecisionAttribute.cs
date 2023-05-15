using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Validations;

public class PrecisionAttribute : ValidationAttribute
{
    public int Precision { get; set; }

    public PrecisionAttribute(int precision)
    {
        Precision = precision;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult(validationContext.DisplayName + " está inválido.");

        if (!decimal.TryParse(value.ToString(), out decimal valor))
            return new ValidationResult(validationContext.DisplayName + " está inválido.");

        decimal precision = 0;
        while (valor > 0)
        {
            precision += valor % 10;
            valor /= 10;
        }


        return precision > Precision
            ? new ValidationResult(validationContext.DisplayName + $" pode ter até {Precision} dígitos.")
            : ValidationResult.Success;
    }
}
