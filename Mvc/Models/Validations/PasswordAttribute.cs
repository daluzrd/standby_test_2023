using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Mvc.Models.Validations;

public class PasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("Senha é obrigatória");
        }

        var convertedValue = (string)value;
        var passwordPattern = "((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,})";
        Regex rg = new(passwordPattern);
        bool isValid = rg.IsMatch(convertedValue);

        return !isValid
            ? new ValidationResult($"Senha deve possuir pelo menos 8 caracteres, um carácter maiúsculo, um carácter minúsculo, um carácter numérico e um carácter especial")
            : ValidationResult.Success;
    }
}
