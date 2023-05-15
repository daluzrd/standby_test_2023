using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity.Models
{
    public record Login (
        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress]
        string Email,
        string Password
    );
}