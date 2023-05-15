using Mvc.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Cliente;

public class ClienteViewModel
{
    public Guid Id { get; private set; }

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Nome deve conter entre 3 e 20 caracteres.")]
    public string Nome { get; private set; }

    [Required(ErrorMessage = "Cpf/Cnpj é obrigatório.")]
    [CpfCnpj]
    public string CpfCnpj { get; private set; }
    public bool Ativo { get; private set; }

    public ClienteViewModel(Guid id, string nome, string cpfCnpj, bool ativo)
    {
        Id = id;
        Nome = nome;
        CpfCnpj = cpfCnpj;
        Ativo = ativo;
    }
}