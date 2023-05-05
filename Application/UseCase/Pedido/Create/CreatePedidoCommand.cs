using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Pedidos.Create;


public class CreatePedidoCommand : ICommand<CreatePedidoCommandResult>
{
    public Guid ClienteId { get; private set; }
    public DateTime Data { get; private set; }
    public char Status { get; private set; }

    public CreatePedidoCommand(Guid clienteId, DateTime data)
    {
        ClienteId = clienteId;
        Data = data;
        Status = 'A';
    }

    public ResultValidator Validate()
    {
        var validator = new Validator<CreatePedidoCommand>();

        validator.RuleFor(p => p.ClienteId)
            .NotEmpty()
            .WithMessage(@"O campo ""ClienteId"" é obrigatório.")
            .NotNull()                
            .WithMessage(@"O campo ""ClienteId"" é obrigatório.");

        validator.RuleFor(p => p.Data)
            .NotEmpty()
            .WithMessage(@"O campo ""Data"" é obrigatório.")
            .NotNull()                
            .WithMessage(@"O campo ""Data"" é obrigatório.")
            .Must(d => d > DateTime.Now)
            .WithMessage(@"Não é possível criar um pedido para o passado.");

        return new ResultValidator(validator.Validate(this));
    }
}