using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Account;

public record RegisterViewModel(
        [Required(ErrorMessage = "Nome é obrigatório.")]
        string Nome,

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        string Email,

        [Required(ErrorMessage = "Senha é obrigatório.")]
        string Password,


        [Required(ErrorMessage = "Senha é obrigatório.")]
        string PasswordConfirm
);