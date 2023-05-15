using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Pedidos.Update;

public class UpdatePedidoCommand : ICommand<ClosePedidoCommandResult>
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public DateTime Data { get; private set; }

    public UpdatePedidoCommand(Guid id, Guid clienteId, DateTime data)
    {
        Id = id;
        ClienteId = clienteId;
        Data = data;
    }

    public ResultValidator Validate()
    {
        var validator = new Validator<UpdatePedidoCommand>();

        validator.RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage(@"O campo ""Id"" não pode ser vazio.")
            .NotNull()
            .WithMessage(@"O campo ""Id"" não pode ser nulo.");

        validator.RuleFor(p => p.ClienteId)
            .NotEmpty()
            .WithMessage(@"O campo ""ClienteId"" não pode ser vazio.")
            .NotNull()
            .WithMessage(@"O campo ""ClienteId"" não pode ser nulo.");

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