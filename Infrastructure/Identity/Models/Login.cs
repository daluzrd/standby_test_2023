using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity.Models
{
    public record Login (
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress]
        string Email,
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        string Password
    );
}