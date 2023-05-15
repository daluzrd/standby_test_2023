using Mvc.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Account;

public record RegisterViewModel(
        [Required(ErrorMessage = "Nome é obrigatório.")]
        string Nome,

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        string Email,

        [Password]
        [Required(ErrorMessage = "Senha é obrigatória.")]
        string Password,

        [Required(ErrorMessage = "Confirmação de senha é obrigatória.")]
        string PasswordConfirm
);