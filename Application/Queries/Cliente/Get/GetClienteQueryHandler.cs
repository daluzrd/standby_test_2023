using Application.Queries.Clientes.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.Clientes.Get;

public class GetClienteQueryHandler : IQueryHandler<GetClienteQueryInput, IEnumerable<GetClienteByIdViewModel>>
{
    private readonly IReadRepository<GetClienteByIdViewModel> _repository;

    public GetClienteQueryHandler(IReadRepository<GetClienteByIdViewModel> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetClienteByIdViewModel>> Handle(GetClienteQueryInput request, CancellationToken cancellationToken)
    {
        var query = @"select c.Id, c.Nome, c.CpfCnpj, c.Ativo from Cliente c";
        var textVariables = new List<string> { "c.Nome", "c.CpfCnpj" };

        IEnumerable<GetClienteByIdViewModel> clientes = await _repository.ExecuteQueryAsync(query);
        clientes = !string.IsNullOrWhiteSpace(request.Filter)
            ? BuildFilters(clientes, request.Filter)
            : clientes;

        return clientes;
    }

    private IEnumerable<GetClienteByIdViewModel> BuildFilters(IEnumerable<GetClienteByIdViewModel> clientes, string filter)
    {
        filter = filter.ToLower();
        clientes = clientes.Where(c => c.Nome.ToLower().Contains(filter) || c.CpfCnpj.ToLower().Contains(filter));

        return clientes;
    }
}