using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Cliente
{
    public record UpdateClienteDto (
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MaxLength(100)]
        string Nome,

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MinLength(11)]
        [MaxLength(14)]
        string CpfCnpj,

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        bool Ativo
    );
}