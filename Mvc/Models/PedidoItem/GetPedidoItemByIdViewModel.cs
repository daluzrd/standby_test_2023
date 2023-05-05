namespace Mvc.Models.PedidoItem;

public class GetPedidoItemByIdViewModel
{
    public Guid Id { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public int Quantidade { get; private set; }

    public GetPedidoItemByIdViewModel(Guid id, Guid pedidoId, int quantidade, Guid produtoId)
    {
        Id = id;
        PedidoId = pedidoId;
        Quantidade = quantidade;
        ProdutoId = produtoId;
    }
}
