using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.PedidoItems.Update
{
    public class UpdatePedidoItemQuantidadeCommand : ICommand<AddItemToPedidoCommandResult>
    {
        public Guid PedidoItemId { get; private set; }
        public int Quantidade { get; private set; }

        public UpdatePedidoItemQuantidadeCommand(Guid pedidoItemId, int quantidade)
        {
            PedidoItemId = pedidoItemId;
            Quantidade = quantidade;
        }

        public ResultValidator Validate()
        {
            var validator = new Validator<UpdatePedidoItemQuantidadeCommand>();

            validator.RuleFor(i => i.PedidoItemId)
                .NotEmpty()
                .WithMessage(@"O campo ""PedidoItemId"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""PedidoItemId"" é obrigatório.");

            validator.RuleFor(i => i.Quantidade)
                .NotEmpty()
                .WithMessage(@"O campo ""Quantidade"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Quantidade"" é obrigatório.")
                .Must(i => i > 0)
                .WithMessage("Quantidade deve ser maior que 0.");

            return new ResultValidator(validator.Validate(this));
        }
    }
}