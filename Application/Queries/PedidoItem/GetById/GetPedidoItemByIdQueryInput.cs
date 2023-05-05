using SharedKernel.Queries;

namespace Application.Queries.PedidoItems.GetById;

public class GetPedidoItemByIdQueryInput : IQuerySingle<GetPedidoItemByIdViewModel>
{
    public Guid Id { get; private set; }

    public GetPedidoItemByIdQueryInput(Guid id)
    {
        Id = id;
    }
}