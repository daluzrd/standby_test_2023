using Application.Queries.Produtos.GetById;
using SharedKernel.Queries;

namespace Application.Queries.PedidoItems.GetById;

public class GetPedidoItemByIdViewModel : QueryResult
{
    public Guid Id { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public int Quantidade { get; private set; }

    public GetPedidoItemByIdViewModel() {}

    public GetPedidoItemByIdViewModel(
        Guid id, 
        Guid pedidoId,
        Guid produtoId, 
        int quantidade
        )
    {
        Id = id;
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }
}
