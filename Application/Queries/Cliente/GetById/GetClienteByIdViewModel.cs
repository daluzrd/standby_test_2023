using SharedKernel.Queries;

namespace Application.Queries.Clientes.GetById;

public class GetClienteByIdViewModel : QueryResult
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string CpfCnpj { get; private set; }
    public bool Ativo { get; private set; }

    public GetClienteByIdViewModel() {}

    public GetClienteByIdViewModel(Guid id, string nome, string cpfCnpj, bool ativo)
    {
        Id = id;
        Nome = nome;
        CpfCnpj = cpfCnpj;
        Ativo = ativo;
    }
}