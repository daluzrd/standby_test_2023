using SharedKernel.Interfaces;

namespace Domain.Models
{
    public class PedidoItem
    {
        public Guid Id { get; private set; }
        public Pedido Pedido { get; private set; } = null!;
        public Produto Produto { get; private set; } = null!;
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal ValorTotal { get; private set; }

        protected PedidoItem(int quantidade, decimal valorUnitario)
        {
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ValorTotal = quantidade * valorUnitario;
        }

        public PedidoItem(Pedido pedido, Produto produto, int quantidade, decimal valorUnitario)
        {
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
            ValorUnitario = produto.Valor;
            ValorTotal = quantidade * valorUnitario;
        }

        public void ChangePedidoQuantity(int quantity)
        {
            Quantidade = quantity;
            ValorTotal = ValorUnitario * Quantidade;
        }
    }
}