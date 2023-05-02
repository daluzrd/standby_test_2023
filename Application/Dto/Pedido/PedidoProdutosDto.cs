using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Pedido
{
    public record PedidoProdutosDto(
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        Guid ProdutoId,
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        int Quantidade
    );
}