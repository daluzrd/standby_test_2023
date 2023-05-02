using Application.Queries.Clientes.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Clientes.Get
{
    public class GetClienteQueryInput : IQuery<IEnumerable<GetClienteByIdViewModel>>
    {
        public string? Nome { get; private set; }
        public string? CpfCnpj { get; private set; }
        public bool? Ativo { get; private set; }
        public string? OrderBy { get; private set; }
        public bool? OrderAsc { get; private set; }

        public GetClienteQueryInput(string? nome, string? cpfCnpj, bool? ativo, string? orderBy, bool? orderAsc)
        {
            Nome = nome;
            CpfCnpj = cpfCnpj;
            Ativo = ativo;
            OrderBy = orderBy;
            OrderAsc = orderAsc;
        }
    }
}