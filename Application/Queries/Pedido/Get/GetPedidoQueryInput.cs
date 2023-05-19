using SharedKernel.Queries;

namespace Application.Queries.Pedidos.Get;

public class GetPedidoQueryInput : IQuery<IEnumerable<GetPedidoViewModel>>
{
    public string Filter { get; private set; }
    public string OrderBy { get; private set; }
    public bool OrderAsc { get; private set; }

    public GetPedidoQueryInput(string filter)
    {
        OrderBy = "id";
        OrderAsc = true;
        Filter = filter;
    }

    public GetPedidoQueryInput(string filter, string orderBy, bool orderAsc)
    {
        Filter = filter;
        OrderBy = orderBy;
        OrderAsc = orderAsc;
    }
}