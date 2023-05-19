using Application.Queries.Produtos.GetById;
using SharedKernel.Interfaces;
using SharedKernel.Queries;
using SharedKernel.Utils;

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

        IEnumerable<GetPedidoItemByPedidoIdViewModel> pedidoItens = await _repository.ExecuteQueryAsync<GetPedidoItemByPedidoIdViewModel, GetProdutoByIdViewModel, GetPedidoItemByPedidoIdViewModel>(
            query,
            (pedidoItem, produto) =>
            {
                pedidoItem.AddProduto(produto);
                return pedidoItem;
            },
            "ValorTotal,Id",
            new { pedidoId = request.PedidoId});            
        pedidoItens = !string.IsNullOrWhiteSpace(request.Filter)
            ? BuildFilters(pedidoItens, request.Filter)
            : pedidoItens;

        pedidoItens = Sort(pedidoItens, request.OrderBy, request.OrderAsc);
            
        return pedidoItens;
    }

    private IEnumerable<GetPedidoItemByPedidoIdViewModel> BuildFilters(IEnumerable<GetPedidoItemByPedidoIdViewModel> pedidoItens, string filter)
    {
        filter = filter.ToLower();
        pedidoItens = pedidoItens.Where(
            pi => pi.Produto.Descricao.ToLower().Contains(filter) || 
            pi.Quantidade.ToString().Contains(filter) || 
            pi.ValorUnitario.ToString().Contains(filter) || 
            pi.ValorTotal.ToString().Contains(filter));

        return pedidoItens;
    }

    private IEnumerable<GetPedidoItemByPedidoIdViewModel> Sort(IEnumerable<GetPedidoItemByPedidoIdViewModel> pedidoItens, string orderBy, bool orderAsc)
    {
        pedidoItens = orderBy.ToLower() switch
        {
            "quantidade" => EnumerableUtils<GetPedidoItemByPedidoIdViewModel>.Sort(pedidoItens, p => p.Quantidade, orderAsc),
            "valorunitario" => EnumerableUtils<GetPedidoItemByPedidoIdViewModel>.Sort(pedidoItens, p => p.ValorUnitario, orderAsc),
            "valortotal" => EnumerableUtils<GetPedidoItemByPedidoIdViewModel>.Sort(pedidoItens, p => p.ValorTotal, orderAsc),
            "produto" => EnumerableUtils<GetPedidoItemByPedidoIdViewModel>.Sort(pedidoItens, p => p.Produto.Descricao, orderAsc),
            _ => EnumerableUtils<GetPedidoItemByPedidoIdViewModel>.Sort(pedidoItens, p => p.Id, orderAsc)
        };

        return pedidoItens;
    }
}