using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Pedido
{
    public record CreatePedidoDto(
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        Guid ClienteId,
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        DateTime Data,
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        List<PedidoProdutosDto> PedidoProdutosDtos
    );
}