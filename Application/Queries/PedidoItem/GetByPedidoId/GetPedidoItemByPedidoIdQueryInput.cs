using SharedKernel.Queries;

namespace Application.Queries.PedidoItems.GetByPedidoId;

public class GetPedidoItemByPedidoIdQueryInput : IQuery<IEnumerable<GetPedidoItemByPedidoIdViewModel>>
{
    public Guid PedidoId { get; private set; }

    public GetPedidoItemByPedidoIdQueryInput(Guid pedidoId)
    {
        PedidoId = pedidoId;
    }
}