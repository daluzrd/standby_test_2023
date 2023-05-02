using SharedKernel.Interfaces;

namespace Domain.Models
{
    public class Produto : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public decimal Valor { get; private set; }
        private readonly List<PedidoItem> _pedidoItens;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItens;

        public Produto(string codigo, string descricao, int quantidadeEstoque, decimal valor)
        {
            Codigo = codigo;
            Descricao = descricao;
            QuantidadeEstoque = quantidadeEstoque;
            Valor = valor;
            _pedidoItens = new();
        }

        public Produto(
            Guid id, 
            string codigo, 
            string descricao, 
            int quantidadeEstoque, 
            decimal valor) : this(codigo, descricao, quantidadeEstoque, valor)
        {
            Id = id;
        }

        public Produto(
            Guid id,
            string codigo, 
            string descricao, 
            int quantidadeEstoque, 
            decimal valor, 
            List<PedidoItem> pedidoItens) : this(id, codigo, descricao, quantidadeEstoque, valor)
        {
            _pedidoItens = pedidoItens;
        }

        public void DeletePedidoItem(PedidoItem pedidoItem)
        {
            _pedidoItens.Remove(pedidoItem);
        }

        public void ChangeProdutoData(string codigo, string descricao, int quantidadeEstoque, decimal valor)
        {
            Codigo = codigo;
            Descricao = descricao;
            QuantidadeEstoque = quantidadeEstoque;
            Valor = valor;
        }

        public void RemoveQuantidadeEstoque(int quantidade)
        {
            QuantidadeEstoque -= quantidade;
        }
    }
}