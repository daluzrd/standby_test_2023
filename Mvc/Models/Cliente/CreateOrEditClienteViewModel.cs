using Mvc.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Cliente;

public class CreateOrEditClienteViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Nome deve conter entre 3 e 20 caracteres.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Cpf é obrigatório")]
    [CpfCnpj]
    public string CpfCnpj { get; set; } = null!;
    public bool Ativo { get; set; }

    public CreateOrEditClienteViewModel() { }

    public CreateOrEditClienteViewModel(Guid id, string nome, string cpfCnpj, bool ativo)
    {
        Id = id;
        Nome = nome;
        CpfCnpj = cpfCnpj;
        Ativo = ativo;
    }
}
