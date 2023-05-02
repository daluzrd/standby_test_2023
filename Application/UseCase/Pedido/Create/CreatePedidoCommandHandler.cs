using Domain.Models;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Pedidos.Create
{
    public class CreatePedidoCommandHandler : ICommandHandler<CreatePedidoCommand, CreatePedidoCommandResult>
    {
        private readonly IRepository<Pedido> _repository;
        private readonly IRepository<Produto> _produtoRepository;
        private readonly IRepository<Cliente> _clienteRepository;

        public CreatePedidoCommandHandler(IRepository<Pedido> repository, IRepository<Produto> produtoRepository, IRepository<Cliente> clienteRepository)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<CreatePedidoCommandResult> Handle(CreatePedidoCommand command, CancellationToken cancellationToken)
        {
            var validator = command.Validate();
            if (!validator.IsValid())
            {
                return new CreatePedidoCommandResult(false, validator.GetErrorMessages());
            }

            var cliente = await _clienteRepository.GetById(command.ClienteId);
            if (cliente == null)
            {
                return new CreatePedidoCommandResult(false, "Cliente não existe.");
            }

            var pedido = new Pedido(cliente, command.Data);
            foreach (var pedidoProdutos in command.PedidoProdutos)
            {
                var produto = await _produtoRepository.GetById(pedidoProdutos.ProdutoId);
                if (produto == null)
                {
                    return new CreatePedidoCommandResult(false, $"Não existe produto com o id {pedidoProdutos.ProdutoId}.");
                }

                pedido.AddPedidoItem(produto, pedidoProdutos.Quantidade);
            }

            _repository.Add(pedido);

            var result = await _repository.Commit(cancellationToken);
            if (!result)
            {
                return new CreatePedidoCommandResult(false, "Não foi possível criar o pedido.");
            }

            return new CreatePedidoCommandResult(true, "Pedido criado com sucesso.", pedido.Id);
        }
    }
}