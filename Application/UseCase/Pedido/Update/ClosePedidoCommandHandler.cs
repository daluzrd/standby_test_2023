using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Pedidos.Update
{
    public class ClosePedidoCommandHandler : ICommandHandler<ClosePedidoCommand, ClosePedidoCommandResult>
    {
        private readonly IRepository<Pedido> _repository;
        private readonly IRepository<Produto> _produtoRepository;

        public ClosePedidoCommandHandler(IRepository<Pedido> repository, IRepository<Produto> produtoRepository)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;
        }

        public async Task<ClosePedidoCommandResult> Handle(ClosePedidoCommand command, CancellationToken cancellationToken)
        {
            var validator = command.Validate();
            if (!validator.IsValid())
            {
                return new ClosePedidoCommandResult(false, validator.GetErrorMessages());
            }

            var pedido = (
                await _repository.Get(p => p.Id == command.Id, 
                pedido => pedido.Include(p => p.PedidoItens)))
            .FirstOrDefault();

            if (pedido == null)
            {
                return new ClosePedidoCommandResult(false, "Pedido não existe.");
            }

            if (pedido.Status != 'A')
            {
                return new ClosePedidoCommandResult(false, "Não é possível fechar um pedido que não está aberto.");
            }

            foreach (var pedidoItem in pedido.PedidoItens)
            {
                var produto = (await _produtoRepository.Get(
                    p => p.PedidoItems.Any(pi => pi.Id == pedidoItem.Id), 
                    produto => produto.Include(p => p.PedidoItems))).FirstOrDefault();

                if (produto == null)
                {
                    return new ClosePedidoCommandResult(false, $"Produto com o Id \"{pedidoItem.Produto.Id}\" não existe.");
                }

                produto.RemoveQuantidadeEstoque(pedidoItem.Quantidade);
                if (produto.QuantidadeEstoque < 0)
                {
                    return new ClosePedidoCommandResult(
                        false, 
                        $"Quantidade no estoque do produto com o Id \"{pedidoItem.Produto.Id}\" não é suficiente.");
                }
            }

            pedido.ClosePedido();
            var result = await _repository.Commit(cancellationToken);
            if (!result)
            {
                return new ClosePedidoCommandResult(false, "Não foi possível atualizar o pedido.");
            }

            return new ClosePedidoCommandResult(true, "Pedido atualizado com sucesso.");
        }
    }
}
