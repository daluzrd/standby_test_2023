using FluentValidation.Results;

namespace SharedKernel.Validators;

public class ResultValidator
{
    private ValidationResult ValidationResult { get; }

    public ResultValidator(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }

    public bool IsValid()
    {
        return ValidationResult.IsValid;
    }

    public List<string> GetErrorMessages()
    {
        if (!ValidationResult.Errors.Any())
        {
            return new List<string>();
        }

        return ValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
    }
}