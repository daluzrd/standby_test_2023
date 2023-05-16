using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Validations;

public class CpfCnpjAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult(errorMessage: "Cpf/Cnpj inválido.");
        }

        var cpfCnpj = value.ToString();
        if (string.IsNullOrWhiteSpace(cpfCnpj))
        {
            return new ValidationResult(errorMessage: "Cpf/Cnpj inválido.");
        }

        return cpfCnpj.Length != 11 && cpfCnpj.Length != 14
            ? new ValidationResult(errorMessage: "Cpf/Cnpj inválido.")
            : ValidationResult.Success;
    }
}
