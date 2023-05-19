using Application.Queries.Produtos.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;
using SharedKernel.Utils;

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
        produtos = Sort(produtos, request.OrderBy, request.OrderAsc);

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

    private IEnumerable<GetProdutoByIdViewModel> Sort(IEnumerable<GetProdutoByIdViewModel> produtos, string orderBy, bool orderAsc)
    {
        produtos = orderBy.ToLower() switch
        {
            "codigo" => EnumerableUtils<GetProdutoByIdViewModel>.Sort(produtos, p => p.Codigo, orderAsc),
            "descricao" => EnumerableUtils<GetProdutoByIdViewModel>.Sort(produtos, p => p.Descricao, orderAsc),
            "quantidadeestoque" => EnumerableUtils<GetProdutoByIdViewModel>.Sort(produtos, p => p.QuantidadeEstoque, orderAsc),
            "valor" => EnumerableUtils<GetProdutoByIdViewModel>.Sort(produtos, p => p.Valor, orderAsc),
            _ => EnumerableUtils<GetProdutoByIdViewModel>.Sort(produtos, p => p.Id, orderAsc)
        };

        return produtos;
    }
}