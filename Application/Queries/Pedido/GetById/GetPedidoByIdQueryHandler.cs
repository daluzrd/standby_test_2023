using Application.Queries.Clientes.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.Pedidos.GetById;

public class GetPedidoByIdQueryHandler : IQuerySingleHandler<GetPedidoByIdQueryInput, GetPedidoByIdViewModel>
{
    private readonly IReadRepository<GetPedidoByIdQueryInput> _repository;

    public GetPedidoByIdQueryHandler(IReadRepository<GetPedidoByIdQueryInput> repository)
    {
        _repository = repository;
    }

    public async Task<GetPedidoByIdViewModel?> Handle(GetPedidoByIdQueryInput request, CancellationToken cancellationToken)
    {
        var query = @"select p.Id, p.Data, p.Status, c.Id from Pedido p
            inner join Cliente c
            on p.ClienteId = c.Id
            where p.Id = @id";

        return (await _repository.ExecuteQueryAsync<GetPedidoByIdViewModel, GetClienteByIdViewModel, GetPedidoByIdViewModel>(
            query,
            (pedido, cliente) =>
            {
                pedido.AddCliente(cliente.Id);
                return pedido;
            },
            splitOn: "Status,Id",
            new { id = request.Id }
            )).FirstOrDefault();
    }
}