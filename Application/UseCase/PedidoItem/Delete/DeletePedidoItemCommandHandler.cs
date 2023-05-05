using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.PedidoItems.Delete;

public class DeletePedidoItemCommandHandler : ICommandHandler<DeletePedidoItemCommand, DeletePedidoItemCommandResult>
{
    private readonly IRepository<Pedido> _pedidoRepository;
    private readonly IRepository<Produto> _produtoRepository;
    private readonly IRepository<PedidoItem> _pedidoItemRepository;

    public DeletePedidoItemCommandHandler(IRepository<Pedido> pedidoRepository, IRepository<Produto> produtoRepository, IRepository<PedidoItem> pedidoItemRepository)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
        _pedidoItemRepository = pedidoItemRepository;
    }

    public async Task<DeletePedidoItemCommandResult> Handle(DeletePedidoItemCommand command, CancellationToken cancellationToken)
    {
        var resultValidator = command.Validate();
        if (!resultValidator.IsValid())
        {
            return new DeletePedidoItemCommandResult(false, resultValidator.GetErrorMessages());
        }
        
        var pedidoItem = (await _pedidoItemRepository.Get(
            pi => pi.Id == command.Id, 
            pi => pi.Include(pi => pi.Pedido).Include(pi => pi.Produto))).FirstOrDefault();
        if (pedidoItem == null)
        {
            return new DeletePedidoItemCommandResult(false, "Item do pedido não existe.");
        }

        var pedido = await _pedidoRepository.GetById(pedidoItem.Pedido.Id);
        if (pedido == null)
        {
            return new DeletePedidoItemCommandResult(false, "Pedido não encontrado.");
        }

        var produto = await _produtoRepository.GetById(pedidoItem.Produto.Id);
        if (produto == null)
        {
            return new DeletePedidoItemCommandResult(false, "Produto não encontrado.");
        }

        pedido.DeletePedidoItem(pedidoItem);
        produto.DeletePedidoItem(pedidoItem);
        _pedidoItemRepository.Delete(pedidoItem);
        
        var result = await _pedidoRepository.Commit(cancellationToken);
        if (!result)
        {
            return new DeletePedidoItemCommandResult(false, "Não foi possível excluir o item do pedido.");
        }

        return new DeletePedidoItemCommandResult(true, "Item do pedido excluído com sucesso.");
    }
}