using SharedKernel.Queries;

namespace Application.Queries.Pedidos.GetById;

public class GetPedidoByIdViewModel : QueryResult
{
    public Guid Id { get; private set; }
    public DateTime Data { get; private set; }
    public char Status { get; private set; }
    public Guid ClienteId { get; private set; }

    public GetPedidoByIdViewModel() {}

    public GetPedidoByIdViewModel(Guid id, DateTime data, char status, Guid clienteId)
    {
        Id = id;
        Data = data;
        Status = status;
        ClienteId = clienteId;
    }

    public void AddCliente(Guid clienteId)
    {
        ClienteId = clienteId;
    }    
}