using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.PedidoItems.Delete;

public class DeletePedidoItemCommand : ICommand<DeletePedidoItemCommandResult>
{
    public Guid Id { get; private set; }

    public DeletePedidoItemCommand(Guid id)
    {
        Id = id;
    }

    public ResultValidator Validate()
    {
        var validator = new Validator<DeletePedidoItemCommand>();

        validator.RuleFor(v => v.Id)
            .NotEmpty()
            .WithMessage(@"O campo ""Id"" é obrigatório.")
            .NotNull()                
            .WithMessage(@"O campo ""Id"" é obrigatório.");

        return new ResultValidator(validator.Validate(this));
    }
}