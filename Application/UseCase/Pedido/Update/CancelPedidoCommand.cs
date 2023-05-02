using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Pedidos.Update
{
    public class CancelPedidoCommand : ICommand<ClosePedidoCommandResult>
    {
        public Guid Id { get; private set; }

        public CancelPedidoCommand(Guid id)
        {
            Id = id;
        }

        public ResultValidator Validate()
        {
            var validator = new Validator<CancelPedidoCommand>();

            validator.RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage(@"O campo ""Id"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Id"" é obrigatório.");

            return new ResultValidator(validator.Validate(this));
        }
    }
}