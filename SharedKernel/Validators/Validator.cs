using FluentValidation;

namespace SharedKernel.Validators;

public class Validator<T>: AbstractValidator<T> where T : class {}