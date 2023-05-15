using Mvc.Models.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Produto
{
    public class ProdutoViewModel
    {
        public Guid Id { get; private set; }

        [MaxLength(10, ErrorMessage = "Código deve possuir até 10 caracteres.")]
        [Required(ErrorMessage = "Código é obrigatório.")]
        public string Codigo { get; private set; }

        [MaxLength(100, ErrorMessage = "Descrição deve possuir até 100 caracteres.")]
        [Required(ErrorMessage = "Descrição é obrigatório.")]
        public string Descricao { get; private set; }

        [IntGreaterOrEqualThanZero]
        [DisplayName("Estoque")]
        public int QuantidadeEstoque { get; private set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Valor é obrigatório.")]
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
