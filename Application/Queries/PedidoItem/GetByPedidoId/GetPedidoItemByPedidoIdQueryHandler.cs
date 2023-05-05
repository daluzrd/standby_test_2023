using Application.Queries.Produtos.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;

namespace Application.Queries.PedidoItems.GetByPedidoId;

public class GetPedidoItemByPedidoIdQueryHandler : IQueryHandler<GetPedidoItemByPedidoIdQueryInput, IEnumerable<GetPedidoItemByPedidoIdViewModel>>
{
    private readonly IReadRepository<GetPedidoItemByPedidoIdViewModel> _repository;

    public GetPedidoItemByPedidoIdQueryHandler(IReadRepository<GetPedidoItemByPedidoIdViewModel> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetPedidoItemByPedidoIdViewModel>> Handle(GetPedidoItemByPedidoIdQueryInput request, CancellationToken cancellationToken)
    {
        var query = @"
            select pi.Id, 
            pi.PedidoId, 
            pi.Quantidade, 
            pi.ValorUnitario, 
            pi.ValorTotal, 
            p.Id, 
            p.Codigo, 
            p.Descricao, 
            p.QuantidadeEstoque, 
            p.Valor
            from PedidoItem pi
            inner join Produto p
            on pi.ProdutoId = p.Id
            where pi.PedidoId = @pedidoId";

        return await _repository.ExecuteQueryAsync<GetPedidoItemByPedidoIdViewModel, GetProdutoByIdViewModel, GetPedidoItemByPedidoIdViewModel>(
            query,
            (pedidoItem, produto) => {
                pedidoItem.AddProduto(produto);
                return pedidoItem;
            },
            "ValorTotal",
            new { pedidoId = request.PedidoId}
        );
    }
}