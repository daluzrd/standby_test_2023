using Application.Queries.Clientes.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;
using SharedKernel.Utils;

namespace Application.Queries.Clientes.Get;

public class GetClienteQueryHandler : IQuerySingleHandler<GetClienteQueryInput, GetClienteViewModel>
{
    private readonly IReadRepository<GetClienteByIdViewModel> _repository;

    public GetClienteQueryHandler(IReadRepository<GetClienteByIdViewModel> repository)
    {
        _repository = repository;
    }

    public async Task<GetClienteViewModel> Handle(GetClienteQueryInput request, CancellationToken cancellationToken)
    {
        var query = @"select c.Id, c.Nome, c.CpfCnpj, c.Ativo from Cliente c";

        IEnumerable<GetClienteByIdViewModel> clientes = await _repository.ExecuteQueryAsync(query);
        int recordsTotal = clientes.Count();

        clientes = !string.IsNullOrWhiteSpace(request.Filter)
            ? BuildFilters(clientes, request.Filter)
            : clientes;
        int recordsFilterd = clientes.Count();

        clientes = Sort(clientes, request.OrderBy, request.OrderAsc);

        return new GetClienteViewModel(recordsTotal, recordsFilterd, clientes);
    }

    private IEnumerable<GetClienteByIdViewModel> BuildFilters(IEnumerable<GetClienteByIdViewModel> clientes, string filter)
    {
        filter = filter.ToLower();
        clientes = clientes.Where(c => c.Nome.ToLower().Contains(filter) || c.CpfCnpj.ToLower().Contains(filter));

        return clientes;
    }

    private IEnumerable<GetClienteByIdViewModel> Sort(IEnumerable<GetClienteByIdViewModel> clientes, string orderBy, bool orderAsc)
    {
        clientes = orderBy.ToLower() switch
        {
            "nome" => EnumerableUtils<GetClienteByIdViewModel>.Sort(clientes, c => c.Nome, orderAsc),
            "cpfcnpj" => EnumerableUtils<GetClienteByIdViewModel>.Sort(clientes, c => c.CpfCnpj, orderAsc),
            "ativo" => EnumerableUtils<GetClienteByIdViewModel>.Sort(clientes, c => c.Ativo, orderAsc),
            _ => EnumerableUtils<GetClienteByIdViewModel>.Sort(clientes, c => c.Id, orderAsc)
        };

        return clientes;
    }
}