using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Validations;

public class CpfCnpjAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return value == null || (((string)value).Length != 11 && ((string)value).Length != 14)
            ? new ValidationResult(errorMessage: "Cpf/Cnpj inválido.")
            : ValidationResult.Success;
    }
}
