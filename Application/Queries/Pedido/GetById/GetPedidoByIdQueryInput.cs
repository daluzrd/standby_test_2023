using SharedKernel.Queries;

namespace Application.Queries.Pedidos.GetById;

public class GetPedidoByIdQueryInput : IQuerySingle<GetPedidoByIdViewModel>
{
    public Guid Id { get; private set; }

    public GetPedidoByIdQueryInput(Guid id)
    {
        Id = id;
    }
}