using Mvc.Models.Produto;

namespace Mvc.Models.Pedido;

public class GetPedidoItemByPedidoIdViewModel
{
    public Guid Id { get; private set; }
    public Guid PedidoId { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public decimal ValorTotal { get; private set; }
    public ProdutoViewModel Produto { get; private set; }

    public GetPedidoItemByPedidoIdViewModel(Guid id, Guid pedidoId, int quantidade, decimal valorUnitario, decimal valorTotal, ProdutoViewModel produto)
    {
        Id = id;
        PedidoId = pedidoId;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        ValorTotal = valorTotal;
        Produto = produto;
    }
}
