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
        var textVariables = new List<string> { "p.Descricao" };
        List<string>? numberVariables = null;

        var filterIsNumber = false;
        if (decimal.TryParse(request.Filter, out decimal numberFilter))
        {
            filterIsNumber = true;
            numberVariables = new List<string> { "pi.Quantidade", "pi.ValorUnitario", "pi.ValorTotal" };
        }

        if (request.Filter != null)
        {
            query = _repository.BuildQueryFilters(query + " and (", textVariables: textVariables, numberVariables: numberVariables) + ")";
        }

        var textFilter = $"%{request.Filter}%";
        return await _repository.ExecuteQueryAsync<GetPedidoItemByPedidoIdViewModel, GetProdutoByIdViewModel, GetPedidoItemByPedidoIdViewModel>(
            query,
            (pedidoItem, produto) =>
            {
                pedidoItem.AddProduto(produto);
                return pedidoItem;
            },
            "ValorTotal,Id",
            request.Filter != null
                ? filterIsNumber
                    ? new { filter = textFilter, numberFilter = numberFilter, pedidoId = request.PedidoId }
                    : new { filter = textFilter, pedidoId = request.PedidoId }
                : new { pedidoId = request.PedidoId }
        );
    }
}