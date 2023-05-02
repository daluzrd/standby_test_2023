using Domain.Models;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.PedidoItems.Update
{
    public class AddItemToPedidoCommandHandler : ICommandHandler<AddItemToPedidoCommand, AddItemToPedidoCommandResult>
    {
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<Produto> _produtoRepository;

        public AddItemToPedidoCommandHandler(IRepository<Pedido> pedidoRepository, IRepository<Produto> produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<AddItemToPedidoCommandResult> Handle(AddItemToPedidoCommand command, CancellationToken cancellationToken)
        {
            var validator = command.Validate();
            if (!validator.IsValid())
            {
                return new AddItemToPedidoCommandResult(false, validator.GetErrorMessages());
            }

            var produto = await _produtoRepository.GetById(command.ProdutoId);
            if (produto == null)
            {
                return new AddItemToPedidoCommandResult(false, "Produto não existe.");
            }

            var pedido = await _pedidoRepository.GetById(command.PedidoId);
            if (pedido == null)
            {
                return new AddItemToPedidoCommandResult(false, "Pedido não existe.");
            }

            pedido.AddPedidoItem(produto, command.Quantidade);
            var result = await _pedidoRepository.Commit(cancellationToken);
            if (!result)
            {
                return new AddItemToPedidoCommandResult(false, "Não foi possível adicionar produto ao pedido");
            }

            return new AddItemToPedidoCommandResult(true, "Produto adicionado com sucesso.");
        }
    }
}