using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Produtos;

public record CreateProdutoDto (
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [MaxLength(10)]
    string Codigo,
            
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [MaxLength(100)]
    string Descricao,
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    int QuantidadeEstoque,
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    decimal Valor
);