namespace Mvc.Models.PedidoItem
{
    public class PedidoItemViewModel
    {
        public Guid Id { get; private set; }
        public string DescricaoProduto { get; private set; } = null!;
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal ValorTotal { get; private set; }

        public PedidoItemViewModel(Guid id, string descricaoProduto, int quantidade, decimal valorUnitario, decimal valorTotal)
        {
            Id = id;
            DescricaoProduto = descricaoProduto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ValorTotal = valorTotal;
        }
    }
}
