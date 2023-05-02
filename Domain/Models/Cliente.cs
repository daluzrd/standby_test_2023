using SharedKernel.Interfaces;

namespace Domain.Models
{
    public class Cliente : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string CpfCnpj { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        private readonly List<Pedido> _pedidos;
        public IReadOnlyCollection<Pedido> Pedidos => _pedidos;

        public Cliente(string cpfCnpj, string nome)
        {
            CpfCnpj = cpfCnpj;
            Nome = nome;
            Ativo = true;
            _pedidos = new();
        }

        public void ChangeClienteData(string cpfCnpj, string nome, bool ativo)
        {
            CpfCnpj = cpfCnpj;
            Nome = nome;
            Ativo = ativo;
        }

        public void AddPedido(Pedido pedido)
        {
            _pedidos.Add(pedido);
        }
    }
}