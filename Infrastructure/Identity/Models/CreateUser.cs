using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity.Models;

public record CreateUser (
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    string Nome,

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress]
    string Email,
    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    string Password,
    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    string PasswordConfirm
);