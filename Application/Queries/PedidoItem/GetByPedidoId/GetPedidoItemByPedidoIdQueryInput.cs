using SharedKernel.Queries;

namespace Application.Queries.PedidoItems.GetByPedidoId;

public class GetPedidoItemByPedidoIdQueryInput : IQuery<IEnumerable<GetPedidoItemByPedidoIdViewModel>>
{
    public Guid PedidoId { get; private set; }
    public string Filter { get; private set; }
    public string OrderBy { get; private set; }
    public bool OrderAsc { get; private set; }

    public GetPedidoItemByPedidoIdQueryInput(Guid pedidoId, string filter)
    {
        PedidoId = pedidoId;
        Filter = filter;
        OrderBy = "id";
        OrderAsc = true;
    }

    public GetPedidoItemByPedidoIdQueryInput(Guid pedidoId, string filter, string orderBy, bool orderAsc)
    {
        PedidoId = pedidoId;
        Filter = filter;
        OrderBy = orderBy;
        OrderAsc = orderAsc;
    }
}