using System.Globalization;
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
        var query = @"select p.Id, p.Data, p.Status, p.Valor, p.DataAtualizacao, c.Nome from Pedido p
            inner join Cliente c
            on p.ClienteId = c.Id";
        var textVariables = new string[] { "c.Nome" };

        bool filterIsNumber = false;
        bool filterIsDate = false;
        if (decimal.TryParse(request.Filter, out decimal numberFilter))
        {
            filterIsNumber = true;
            var numberVariables = new string[] { "p.Valor" };
            query += request.Filter != null
            ? $" where {_repository.BuildQueryFilters(textVariables: textVariables, numberVariables: numberVariables)}"
            : string.Empty;
        }
        else if (DateTime.TryParseExact(request.Filter, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime dateFilter))
        {
            filterIsDate = true;
            var dateVariables = new string[] { "p.Data", "p.DataAtualizacao" };
            query += request.Filter != null
            ? $" where {_repository.BuildQueryFilters(textVariables: textVariables, dateVariables: dateVariables)}"
            : string.Empty;
        }
        else
        {
            query += request.Filter != null
            ? $" where {_repository.BuildQueryFilters(textVariables: textVariables)}"
            : string.Empty;
        }

        var textFilter = $"%{request.Filter}%";

        return await _repository.ExecuteQueryAsync<GetPedidoViewModel, GetClienteByIdViewModel, GetPedidoViewModel>(
            query,
            (pedido, cliente) =>
            {
                pedido.AddNomeCliente(cliente.Nome);
                return pedido;
            },
            "Nome",
            request != null
            ? filterIsNumber
                ? new { filter = textFilter, numberFilter = request.Filter }
                : filterIsDate
                    ? new { filter = textFilter, dateFilter = DateTime.ParseExact(request.Filter!, "dd/MM/yyyy", null)}
                    : new { filter = textFilter }
            : null);
    }
}