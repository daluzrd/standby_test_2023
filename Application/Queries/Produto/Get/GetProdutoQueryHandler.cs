using Application.Queries.Produtos.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.Get
{
    public class GetProdutoQueryHandler : IQueryHandler<GetProdutoQueryInput, IEnumerable<GetProdutoByIdViewModel>>
    {
        private readonly IReadRepository<GetProdutoByIdViewModel> _readRepository;

        public GetProdutoQueryHandler(IReadRepository<GetProdutoByIdViewModel> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<IEnumerable<GetProdutoByIdViewModel>> Handle(GetProdutoQueryInput request, CancellationToken cancellationToken)
        {
            var query = @"select p.Id, p.Codigo, p.Descricao, p.QuantidadeEstoque, p.Valor from Produto p";
            query = AddFiltersToQuery(request, query);

            if (!string.IsNullOrWhiteSpace(request.OrderBy) || request.OrderAsc != null)
            {
                query = AddSortToQuery(request, query);
            }
            return await _readRepository.ExecuteQueryAsync(query);
        }
        

        private string AddFiltersToQuery(GetProdutoQueryInput request, string query)
        {
            var wasAddedFilter = false;

            if (request.Codigo != null || request.Descricao != null || request.QuantidadeEstoque != null || request.Valor != null)
            {
                query += " where";

                if (request.Codigo != null) 
                {
                    query += $" Codigo = {request.Codigo}";
                }
                if (!string.IsNullOrWhiteSpace(request.Descricao))
                {
                    query += $"{(wasAddedFilter ? " and" : "")} Descricao like %{request.Descricao}%";
                }
                if (request.QuantidadeEstoque != null)
                {
                    query += $"{(wasAddedFilter ? " and" : "")} QuantidadeEstoque like %{request.QuantidadeEstoque}%";
                }
                if (request.Valor != null)
                {
                    query += $"{(wasAddedFilter ? " and" : "")} Valor like %{request.Valor}%";
                }
            }

            return query;
        }
        
        private string AddSortToQuery(GetProdutoQueryInput request, string query)
        {
            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                throw new ArgumentException(@"O parâmetro ""orderBy"" está inválido.");
            }

            switch(request.OrderBy!.ToLower())
            {
                case "codigo":
                    if (request.OrderAsc != null)
                    {
                        return query += $"order by Codigo {((bool)request.OrderAsc ? "asc" : "desc")}";
                    }
                    throw new ArgumentException(@"O parâmetro ""orderAsc"" recebe apenas ""true"" ou ""false"".");
                case "descricao":
                    if (request.OrderAsc != null)
                    {
                        return query += $"order by Descricao {((bool)request.OrderAsc ? "asc" : "desc")}";
                    }
                    throw new ArgumentException(@"O parâmetro ""orderAsc"" recebe apenas ""true"" ou ""false"".");
                case "quantidadeestoque":
                    if (request.OrderAsc != null)
                    {
                        return query += $"order by QuantidadeEstoque {((bool)request.OrderAsc ? "asc" : "desc")}";
                    }
                    throw new ArgumentException(@"O parâmetro ""orderAsc"" recebe apenas ""true"" ou ""false"".");
                case "valor":
                    if (request.OrderAsc != null)
                    {
                        return query += $"order by Valor {((bool)request.OrderAsc ? "asc" : "desc")}";
                    }
                    throw new ArgumentException(@"O parâmetro ""orderAsc"" recebe apenas ""true"" ou ""false"".");
                default:
                    throw new ArgumentException(@"O parâmetro ""orderBy"" está inválido.");
            }
        }
    }
}