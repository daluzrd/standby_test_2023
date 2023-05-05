using Application.Queries.Clientes.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Clientes.Get;

public class GetClienteQueryInput : IQuery<IEnumerable<GetClienteByIdViewModel>>
{
    public GetClienteQueryInput() {}
}