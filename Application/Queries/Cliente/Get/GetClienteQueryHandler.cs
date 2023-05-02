using Application.Queries.Clientes.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.Clientes.Get
{
    public class GetClienteQueryHandler : IQueryHandler<GetClienteQueryInput, IEnumerable<GetClienteByIdViewModel>>
    {
        private readonly IReadRepository<GetClienteByIdViewModel> _clienteReadRepository;

        public GetClienteQueryHandler(IReadRepository<GetClienteByIdViewModel> clienteReadRepository)
        {
            _clienteReadRepository = clienteReadRepository;
        }

        public async Task<IEnumerable<GetClienteByIdViewModel>> Handle(GetClienteQueryInput request, CancellationToken cancellationToken)
        {
            var query = @"select c.Id, c.Nome, c.CpfCnpj, c.Ativo from Cliente c";
            query = AddFiltersToQuery(request, query);

            if (!string.IsNullOrWhiteSpace(request.OrderBy) || request.OrderAsc != null)
            {
                query = AddSortToQuery(request, query);
            }

            return await _clienteReadRepository.ExecuteQueryAsync(query, cancellationToken);
        }

        private string AddFiltersToQuery(GetClienteQueryInput request, string query)
        {
            var wasAddedFilter = false;

            if (request.Ativo != null || request.CpfCnpj != null || request.Nome != null)
            {
                query += " where";

                if (request.Ativo != null) 
                {
                    query += $" Ativo = {request.Ativo}";
                }
                if (!string.IsNullOrWhiteSpace(request.CpfCnpj))
                {
                    query += $"{(wasAddedFilter ? " and" : "")} CpfCnpj like %{request.CpfCnpj}%";
                }
                if (!string.IsNullOrWhiteSpace(request.Nome))
                {
                    query += $"{(wasAddedFilter ? " and" : "")} Nome like %{request.Nome}%";
                }
            }

            return query;
        }

        private string AddSortToQuery(GetClienteQueryInput request, string query)
        {
            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                throw new ArgumentException(@"O parâmetro ""orderBy"" está inválido.");
            }

            switch(request.OrderBy!.ToLower())
            {
                case "nome":
                    if (request.OrderAsc != null)
                    {
                        return query += $"order by Nome {((bool)request.OrderAsc ? "asc" : "desc")}";
                    }
                    throw new ArgumentException(@"O parâmetro ""orderAsc"" recebe apenas ""true"" ou ""false"".");
                case "cpfcnpj":
                    if (request.OrderAsc != null)
                    {
                        return query += $"order by CpfCnpj {((bool)request.OrderAsc ? "asc" : "desc")}";
                    }
                    throw new ArgumentException(@"O parâmetro ""orderAsc"" recebe apenas ""true"" ou ""false"".");
                case "ativo":
                    if (request.OrderAsc != null)
                    {
                        return query += $"order by Ativo {((bool)request.OrderAsc ? "asc" : "desc")}";
                    }
                    throw new ArgumentException(@"O parâmetro ""orderAsc"" recebe apenas ""true"" ou ""false"".");
                default:
                    throw new ArgumentException(@"O parâmetro ""orderBy"" está inválido.");
            }
        }
    }
}