using Application.Queries.Produtos.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.Get;

public class GetProdutoQueryInput : IQuery<IEnumerable<GetProdutoByIdViewModel>>
{
    public GetProdutoQueryInput() {}
}