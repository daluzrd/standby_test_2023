using System.ComponentModel.DataAnnotations;

namespace Application.Dto.PedidoItem
{
    public record UpdatePedidoItemQuantidadeDto (
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        int Quantidade
    );
}