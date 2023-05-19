using System.Globalization;
using Application.Queries.Clientes.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;
using SharedKernel.Utils;

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
        var query = @"select p.Id, p.Data, p.Status, p.Valor, p.DataAtualizacao, c.Nome from Pedido p
            inner join Cliente c
            on p.ClienteId = c.Id";

        IEnumerable<GetPedidoViewModel> pedidos = await _repository.ExecuteQueryAsync<GetPedidoViewModel, GetClienteByIdViewModel, GetPedidoViewModel>(
            query,
            (pedido, cliente) =>
            {
                pedido.AddNomeCliente(cliente.Nome);
                return pedido;
            },
            "Nome");
        pedidos = !string.IsNullOrWhiteSpace(request.Filter) ?
            BuildFilters(pedidos, request.Filter)
            : pedidos;

        return pedidos;
    }

    private IEnumerable<GetPedidoViewModel> BuildFilters(IEnumerable<GetPedidoViewModel> pedidos, string filter)
    {
        filter = filter.ToLower();
        pedidos = pedidos.Where(
            p => p.NomeCliente.ToLower().Contains(filter) || 
            p.Valor.ToString().Contains(filter) ||
            DateTimeUtils.GetBrazilianDateString(p.Data).Contains(filter) || 
            DateTimeUtils.GetBrazilianDateString(p.DataAtualizacao).Contains(filter));

        return pedidos;
    }
}