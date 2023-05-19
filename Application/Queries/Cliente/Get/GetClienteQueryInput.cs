using Application.Queries.Clientes.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Clientes.Get;

public class GetClienteQueryInput : IQuerySingle<GetClienteViewModel>
{
    public string Filter { get; private set; }
    public string OrderBy { get; private set; }
    public bool OrderAsc { get; private set; }
    public GetClienteQueryInput(string filter)
    {
        Filter = filter;
        OrderBy = "id";
        OrderAsc = true;
    }

    public GetClienteQueryInput(string filter, string orderBy, bool orderAsc)
    {
        Filter= filter;
        OrderBy = orderBy;
        OrderAsc = orderAsc;
    }
}