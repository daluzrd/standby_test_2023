using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.PedidoItems.Update
{
    public class AddItemToPedidoCommand : ICommand<AddItemToPedidoCommandResult>
    {
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

        public AddItemToPedidoCommand(Guid pedidoId, Guid produtoId, int quantidade)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public ResultValidator Validate()
        {
            var validator = new Validator<AddItemToPedidoCommand>();

            validator.RuleFor(i => i.PedidoId)
                .NotEmpty()
                .WithMessage(@"O campo ""PedidoId"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""PedidoId"" é obrigatório.");

            validator.RuleFor(i => i.ProdutoId)
                .NotEmpty()
                .WithMessage(@"O campo ""ProdutoId"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""ProdutoId"" é obrigatório.");

            validator.RuleFor(i => i.Quantidade)
                .NotEmpty()
                .WithMessage(@"O campo ""Quantidade"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Quantidade"" é obrigatório.")
                .Must(i => i > 0)
                .WithMessage(@"Quantidade deve ser maior que zero");

            return new ResultValidator(validator.Validate(this));
        }
    }
}