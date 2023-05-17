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
        var textVariables = new string[] { "p.Codigo", "p.Descricao" };

        var filterIsNumber = false;
        if (decimal.TryParse(request.Filter, out decimal numberFilter))
        {
            filterIsNumber = true;
            var numberVariables = new string[] { "p.QuantidadeEstoque", "p.Valor" };
            query += request.Filter != null
                ? $" and ({_repository.BuildQueryFilters(textVariables: textVariables, numberVariables: numberVariables)})"
                : string.Empty;
        }
        else
        {
            var numberVariables = new string[] { "p.QuantidadeEstoque", "p.Valor" };
            query += request.Filter != null
                ? $" and ({_repository.BuildQueryFilters(textVariables: textVariables)})"
                : string.Empty;
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