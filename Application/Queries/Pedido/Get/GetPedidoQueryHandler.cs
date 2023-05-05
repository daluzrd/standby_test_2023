using Application.Queries.Clientes.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.Pedidos.Get;

public class GetPedidoQueryHandler : IQueryHandler<GetPedidoQueryInput, IEnumerable<GetPedidoViewModel>>
{
    private readonly IReadRepository<GetPedidoViewModel> _repository;

    public GetPedidoQueryHandler(IReadRepository<GetPedidoViewModel> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetPedidoViewModel>> Handle(GetPedidoQueryInput request, CancellationToken cancellationToken)
    {
        var query = @"select p.Id, p.Data, p.Status, p.Valor, c.* from Pedido p
            inner join Cliente c
            on p.ClienteId = c.Id";

        return await _repository.ExecuteQueryAsync<GetPedidoViewModel, GetClienteByIdViewModel, GetPedidoViewModel>(
            query, 
            (pedido, cliente) => {
                pedido.AddNomeCliente(cliente.Nome);
                return pedido;
            }, 
            "Valor");
    }
}