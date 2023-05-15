using SharedKernel.Interfaces;

namespace Domain.Models;

public class Pedido : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Cliente Cliente { get; private set; } = null!;
    public DateTime Data { get; private set; }
    public char Status { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataAtualizacao { get; private set; }
    private readonly List<PedidoItem> _pedidoItens;
    public IReadOnlyCollection<PedidoItem> PedidoItens => _pedidoItens;

    protected Pedido(DateTime data)
    {
        Data = data;
        Status = 'A';
        Valor = 0;
        DataAtualizacao = new();
        _pedidoItens = new();
    }

    public Pedido(Cliente cliente, DateTime data) : this(data)
    {
        Cliente = cliente;
    }

    public void ClosePedido()
    {
        Status = 'F';
    }

    public void CancelPedido()
    {
        Status = 'C';
    }

    public void UpdateData(DateTime data)
    {
        Data = data;
    }

    public void UpdateDataAtualizacao()
    {
        DataAtualizacao = DateTime.Now;
    }

    public void AddPedidoItem(Produto produto, int quantidade)
    {
        var pedidoItem = new PedidoItem(this, produto, quantidade, produto.Valor);
        _pedidoItens.Add(pedidoItem);

        Valor += produto.Valor * quantidade;
    }

    public void UpdatePedidoItemQuantidade(int quantidade)
    {
        bool wasAdded = false;
        foreach (var item in _pedidoItens)
        {
            if (wasAdded) 
            {
                Valor += item.ValorTotal;
            }
            else
            {
                Valor = item.ValorTotal;
            }
        }
    }

    public void DeletePedidoItem(PedidoItem pedidoItem)
    {
        _pedidoItens.Remove(pedidoItem);
    }

    public void RemovePedidoItem(PedidoItem pedidoItem)
    {
        _pedidoItens.Remove(pedidoItem);
        
        Valor -= pedidoItem.ValorUnitario * pedidoItem.Quantidade;
    }

    public void UpdateCliente(Cliente cliente)
    {
        Cliente = cliente;
    }
}