using SharedKernel.Queries;

namespace Application.Queries.Pedidos.Get;

public class GetPedidoQueryInput : IQuery<IEnumerable<GetPedidoViewModel>>
{
    public string Filter { get; private set; }

    public GetPedidoQueryInput(string filter)
    {
        Filter = filter;
    }
}