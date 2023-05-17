using Application.Queries.Produtos.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.Get;

public class GetProdutoQueryHandler : IQueryHandler<GetProdutoQueryInput, IEnumerable<GetProdutoByIdViewModel>>
{
    private readonly IReadRepository<GetProdutoByIdViewModel> _repository;

    public GetProdutoQueryHandler(IReadRepository<GetProdutoByIdViewModel> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetProdutoByIdViewModel>> Handle(GetProdutoQueryInput request, CancellationToken cancellationToken)
    {
        var query = @"select p.Id, p.Codigo, p.Descricao, p.QuantidadeEstoque, p.Valor from Produto p";
        var textVariables = new List<string> { "p.Codigo", "p.Descricao" };
        List<string>? numberVariables = null;

        var filterIsNumber = false;
        if (decimal.TryParse(request.Filter, out decimal numberFilter))
        {
            filterIsNumber = true;
            numberVariables = new List<string> { "p.QuantidadeEstoque", "p.Valor" };
        }

        if (request.Filter != null)
        {
            query = _repository.BuildQueryFilters(query + " where ", textVariables: textVariables, numberVariables: numberVariables);
        }

        var textFilter = $"%{request.Filter}%";

        return await _repository.ExecuteQueryAsync(
            query,
            request.Filter != null
            ? filterIsNumber
                ? new { filter = textFilter, numberFilter = request.Filter }
                : new { filter = textFilter }
            : null);
    }
}