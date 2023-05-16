using Mvc.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Produto;

public class CreateOrEditProdutoViewModel
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Codigo { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Descricao { get; set; } = null!;

    [Required]
    [IntGreaterOrEqualThanZero]
    public int QuantidadeEstoque { get; set; }

    [Required]
    [Precision(18)]
    [DecimalGreaterThanZero]
    public decimal Valor { get; set; }

    public CreateOrEditProdutoViewModel() { }

    public CreateOrEditProdutoViewModel(Guid id, string codigo, string descricao, int quantidadeEstoque, decimal valor)
    {
        Id = id;
        Codigo = codigo;
        Descricao = descricao;
        QuantidadeEstoque = quantidadeEstoque;
        Valor = valor;
    }
}
