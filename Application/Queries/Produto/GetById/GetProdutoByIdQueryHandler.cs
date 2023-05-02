using Application.Queries.Produtos.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.GetById
{
    public class GetProdutoByIdQueryHandler : IQuerySingleHandler<GetProdutoByIdQueryInput, GetProdutoByIdViewModel>
    {
        private readonly IReadRepository<GetProdutoByIdViewModel> _readRepository;

        public GetProdutoByIdQueryHandler(IReadRepository<GetProdutoByIdViewModel> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<GetProdutoByIdViewModel> Handle(GetProdutoByIdQueryInput request, CancellationToken cancellationToken)
        {
            var query = @"select p.Id, p.Codigo, p.Descricao, p.QuantidadeEstoque, p.Valor from Produto p Where p.Id = @id";
            return await _readRepository.ExecuteQueryFirstOrDefaultAsync(query, new { id = request.Id});
        }
    }
}