using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.Clientes.GetById
{
    public class GetClienteByIdQueryHandler : IQuerySingleHandler<GetClienteByIdQueryInput, GetClienteByIdViewModel>
    {
        private readonly IReadRepository<GetClienteByIdViewModel> _clienteReadRepository;

        public GetClienteByIdQueryHandler(IReadRepository<GetClienteByIdViewModel> clienteReadRepository)
        {
            _clienteReadRepository = clienteReadRepository;
        }

        public async Task<GetClienteByIdViewModel> Handle(GetClienteByIdQueryInput request, CancellationToken cancellationToken)
        {
            var query = $"select c.Id, c.Nome, c.CpfCnpj, c.Ativo from Cliente c where c.Id = @id";
            return await _clienteReadRepository.ExecuteQueryFirstOrDefaultAsync(query, new { id = request.Id });
        }
    }
}