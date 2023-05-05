using Application.Queries.Produtos.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.PedidoItems.GetById;

public class GetPedidoItemByIdQueryHandler : IQuerySingleHandler<GetPedidoItemByIdQueryInput, GetPedidoItemByIdViewModel>
{
    private readonly IReadRepository<GetPedidoItemByIdViewModel> _repository;

    public GetPedidoItemByIdQueryHandler(IReadRepository<GetPedidoItemByIdViewModel> repository)
    {
        _repository = repository;
    }

    public async Task<GetPedidoItemByIdViewModel> Handle(GetPedidoItemByIdQueryInput request, CancellationToken cancellationToken)
    {
        var query = @"
            select pi.Id, 
            pi.PedidoId, 
            pi.ProdutoId,
            pi.Quantidade
            from PedidoItem pi
            where pi.Id = @id";

        return await _repository.ExecuteQueryFirstOrDefaultAsync(query, new { id = request.Id});
    }
}