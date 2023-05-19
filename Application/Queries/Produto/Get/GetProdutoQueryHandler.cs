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
        
        IEnumerable<GetProdutoByIdViewModel> produtos = await _repository.ExecuteQueryAsync(query);
        produtos = !string.IsNullOrWhiteSpace(request.Filter) ? BuildFilters(produtos, request.Filter) : produtos;

        return produtos;
    }

    private IEnumerable<GetProdutoByIdViewModel> BuildFilters(IEnumerable<GetProdutoByIdViewModel> produtos, string filter)
    {
        filter = filter.ToLower();
        produtos = produtos.Where(
            p => p.Codigo.ToLower().Contains(filter) || 
            p.Descricao.ToLower().Contains(filter) || 
            p.Valor.ToString().Contains(filter));

        return produtos;
    }
}