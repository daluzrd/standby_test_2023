using Application.Queries.Clientes.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Clientes.Get;

public class GetClienteQueryInput : IQuery<IEnumerable<GetClienteByIdViewModel>>
{
    public string Filter { get; private set; }

    public GetClienteQueryInput(string filter)
    {
        Filter = filter;
    }
}