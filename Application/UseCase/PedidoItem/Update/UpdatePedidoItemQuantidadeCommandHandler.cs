using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.PedidoItems.Update;

public class UpdatePedidoItemQuantidadeCommandHandler : ICommandHandler<UpdatePedidoItemQuantidadeCommand, AddItemToPedidoCommandResult>
{
    private readonly IRepository<Pedido> _pedidoRepository;
    private readonly IRepository<Produto> _produtoRepository;

    public UpdatePedidoItemQuantidadeCommandHandler(IRepository<Pedido> pedidoRepository, IRepository<Produto> produtoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<AddItemToPedidoCommandResult> Handle(UpdatePedidoItemQuantidadeCommand command, CancellationToken cancellationToken)
    {
        var resultValidator = command.Validate();
        if (!resultValidator.IsValid())
        {
            return new AddItemToPedidoCommandResult(false, resultValidator.GetErrorMessages());
        }

        var pedido = (await _pedidoRepository.Get(
            p => p.PedidoItens.Any(pi => pi.Id == command.PedidoItemId), 
            pedido => pedido.Include(p => p.PedidoItens)))
            .FirstOrDefault();
        if (pedido == null)
        {
            return new AddItemToPedidoCommandResult(false, "PedidoItem não existe.");
        }

        var pedidoItem = pedido.PedidoItens.Where(pi => pi.Id == command.PedidoItemId).FirstOrDefault();
        if (pedidoItem == null)
        {
            return new AddItemToPedidoCommandResult(false, "PedidoItem não existe.");
        }

        pedidoItem.ChangePedidoQuantity(command.Quantidade);
        pedido.UpdatePedidoItemQuantidade();

        var result = await _pedidoRepository.Commit(cancellationToken);
        if (!result)
        {
            return new AddItemToPedidoCommandResult(false, "Não foi possível excluir o PedidoItem");
        }

        return new AddItemToPedidoCommandResult(true, "Quantidade de Itens do pedido atualizados com sucesso.");
    }
}