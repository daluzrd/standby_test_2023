

using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Pedido
{
    public class PedidoViewModel
    {
        public Guid Id { get; private set; }
        public string NomeCliente { get; private set; }
        public DateTime Data { get; private set; }
        public char Status { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataAtualizacao { get; private set; }

        public PedidoViewModel(Guid id, string nomeCliente, DateTime data, char status, decimal valor, DateTime dataAtualizacao)
        {
            Id = id;
            NomeCliente = nomeCliente;
            Data = data;
            Status = status;
            Valor = valor;
            DataAtualizacao = dataAtualizacao;
        }
    }
}
