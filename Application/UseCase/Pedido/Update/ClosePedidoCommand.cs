using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Pedidos.Update
{
    public class ClosePedidoCommand : ICommand<ClosePedidoCommandResult>
    {
        public Guid Id { get; private set; }

        public ClosePedidoCommand(Guid id)
        {
            Id = id;
        }

        public ResultValidator Validate()
        {
            var validator = new Validator<ClosePedidoCommand>();

            validator.RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage(@"O campo ""Id"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Id"" é obrigatório.");

            return new ResultValidator(validator.Validate(this));
        }
    }
}