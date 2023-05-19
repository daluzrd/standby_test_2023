using Application.Queries.Produtos.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.Get;

public class GetProdutoQueryInput : IQuery<IEnumerable<GetProdutoByIdViewModel>>
{
    public string Filter { get; private set; }
    public string OrderBy { get; private set; }
    public bool OrderAsc { get; private set; }

    public GetProdutoQueryInput(string filter)
    {
        OrderBy = "id";
        OrderAsc = true;
        Filter = filter;
    }

    public GetProdutoQueryInput(string filter, string orderBy, bool orderAsc)
    {
        OrderBy = orderBy;
        OrderAsc = orderAsc;
        Filter = filter;
    }
}