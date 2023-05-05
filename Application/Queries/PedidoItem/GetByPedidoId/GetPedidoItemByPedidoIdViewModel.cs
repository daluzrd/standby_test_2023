using Application.Queries.Produtos.GetById;
using SharedKernel.Queries;

namespace Application.Queries.PedidoItems.GetByPedidoId;

public class GetPedidoItemByPedidoIdViewModel : QueryResult
{
    public Guid Id { get; private set; }
    public Guid PedidoId { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public decimal ValorTotal { get; private set; }
    public GetProdutoByIdViewModel Produto { get; private set; }

    public GetPedidoItemByPedidoIdViewModel() {}

    public GetPedidoItemByPedidoIdViewModel(
        Guid id, 
        Guid pedidoId, 
        int quantidade, 
        decimal valorUnitario, 
        decimal valorTotal, 
        GetProdutoByIdViewModel produto)
    {
        Id = id;
        PedidoId = pedidoId;
        Produto = produto;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        ValorTotal = valorTotal;
    }

    public void AddProduto(GetProdutoByIdViewModel produto)
    {
        Produto = produto;
    }
}
