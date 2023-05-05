using Application.Queries.Produtos.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.GetById;

public class GetProdutoByIdQueryHandler : IQuerySingleHandler<GetProdutoByIdQueryInput, GetProdutoByIdViewModel>
{
    private readonly IReadRepository<GetProdutoByIdViewModel> _repository;

    public GetProdutoByIdQueryHandler(IReadRepository<GetProdutoByIdViewModel> repository)
    {
        _repository = repository;
    }

    public async Task<GetProdutoByIdViewModel> Handle(GetProdutoByIdQueryInput request, CancellationToken cancellationToken)
    {
        var query = @"select p.Id, p.Codigo, p.Descricao, p.QuantidadeEstoque, p.Valor from Produto p Where p.Id = @id";
        return await _repository.ExecuteQueryFirstOrDefaultAsync(query, new { id = request.Id});
    }
}