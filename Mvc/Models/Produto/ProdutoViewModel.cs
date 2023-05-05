using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Produto
{
    public class ProdutoViewModel
    {
        public Guid Id { get; private set; }

        [MaxLength(10)]
        public string Codigo { get; private set; }

        [MaxLength(100)]
        public string Descricao { get; private set; }
        public int QuantidadeEstoque { get; private set; }

        [MaxLength(20)]
        public decimal Valor { get; private set; }

        public ProdutoViewModel(Guid id, string codigo, string descricao, int quantidadeEstoque, decimal valor)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            QuantidadeEstoque = quantidadeEstoque;
            Valor = valor;
        }
    }
}
