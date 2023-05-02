using Application.Queries.Produtos.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.GetById
{
    public class GetProdutoByIdQueryInput : IQuerySingle<GetProdutoByIdViewModel>
    {
        public Guid Id { get; private set; }

        public GetProdutoByIdQueryInput(Guid id)
        {
            Id = id;
        }
    }

}