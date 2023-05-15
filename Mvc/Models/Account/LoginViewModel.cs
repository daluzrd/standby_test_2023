using Mvc.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Account;

public record LoginViewModel(
    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    string Email,

    [Required(ErrorMessage = "Senha é obrigatória.")]
    string Password
);