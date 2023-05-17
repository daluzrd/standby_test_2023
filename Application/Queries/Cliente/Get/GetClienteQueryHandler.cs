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
        var textVariables = new string[] { "c.Nome", "c.CpfCnpj" };
        query += request.Filter != null
        ? $" where {_repository.BuildQueryFilters(textVariables: textVariables)}"
        : string.Empty;

        var textFilter = $"%{request.Filter}%";

        return await _repository.ExecuteQueryAsync(
            query, 
            request.Filter != null 
            ? new { filter = textFilter } 
            : null);
    }
}