namespace Mvc.Models.Pedido;

public class CreateOrEditPedidoViewModel
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public char Status { get; set; }
    public Guid ClienteId { get; set; }

    public CreateOrEditPedidoViewModel() { }

    public CreateOrEditPedidoViewModel(Guid id, Guid clienteId, DateTime data, char status)
    {
        Id = id;
        ClienteId = clienteId;
        Data = data;
        Status = status;
    }
}
