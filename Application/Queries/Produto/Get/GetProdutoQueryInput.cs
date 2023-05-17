using Application.Queries.Produtos.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.Get;

public class GetProdutoQueryInput : IQuery<IEnumerable<GetProdutoByIdViewModel>>
{
    public string Filter { get; private set; }

    public GetProdutoQueryInput(string filter)
    {
        Filter = filter;
    }
}