namespace Mvc.Models.Cliente;

public class ClienteViewModel
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
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