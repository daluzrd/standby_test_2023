using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Pedidos.Create
{
    public class PedidoProdutos
    {
        public int Quantidade { get; private set; }
        public Guid ProdutoId { get; private set; }

        public PedidoProdutos(int quantidade, Guid produtoId)
        {
            Quantidade = quantidade;
            ProdutoId = produtoId;
        }
    }

    public class CreatePedidoCommand : ICommand<CreatePedidoCommandResult>
    {
        public Guid ClienteId { get; private set; }
        public DateTime Data { get; private set; }
        public char Status { get; private set; }
        public List<PedidoProdutos> PedidoProdutos { get; private set; }

        public CreatePedidoCommand(Guid clienteId, DateTime data)
        {
            ClienteId = clienteId;
            Data = data;
            Status = 'A';
            PedidoProdutos = new();
        }

        public CreatePedidoCommand(Guid clienteId, DateTime data, List<PedidoProdutos> pedidoProdutos)
        {
            ClienteId = clienteId;
            Data = data;
            Status = 'A';
            PedidoProdutos = pedidoProdutos;
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

            validator.RuleFor(p => p.PedidoProdutos)
                .NotEmpty()
                .WithMessage(@"O campo ""PedidoProdutos"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""PedidoProdutos"" é obrigatório.")
                .Must(p => p.Count > 0)
                .WithMessage("Não é possível criar um pedido sem produtos.")
                .Must(p => p.All(p => p.Quantidade > 0))
                .WithMessage("Quantidade do produto não pode ser vazia.")
                .Must(p => p.All(p => p.ProdutoId != Guid.Empty))
                .WithMessage("Id do produto é obrigatório");

            return new ResultValidator(validator.Validate(this));
        }
    }
}