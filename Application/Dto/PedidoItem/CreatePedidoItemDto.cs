using System.ComponentModel.DataAnnotations;

namespace Application.Dto.PedidoItem
{
    public record AddItemToPedidoDto (
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        Guid PedidoId,

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        Guid ProdutoId,
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        int Quantidade
    );
}