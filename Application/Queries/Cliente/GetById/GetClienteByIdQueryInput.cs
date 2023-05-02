using SharedKernel.Queries;

namespace Application.Queries.Clientes.GetById
{
    public class GetClienteByIdQueryInput : IQuerySingle<GetClienteByIdViewModel>
    {
        public Guid Id { get; private set; }

        public GetClienteByIdQueryInput(Guid id)
        {
            Id = id;
        }
    }
}