using Application.Queries.Clientes.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Clientes.Get;

public class GetClienteViewModel : QueryResult
{
    public int RecordsTotal { get; private set; }
    public int RecordsFiltered { get; private set; }
    public IEnumerable<GetClienteByIdViewModel> Data { get; private set; }

    public GetClienteViewModel(int recordsTotal, int recordsFiltered, IEnumerable<GetClienteByIdViewModel> data)
    {
        RecordsTotal = recordsTotal;
        RecordsFiltered = recordsFiltered;
        Data = data;
    }
}