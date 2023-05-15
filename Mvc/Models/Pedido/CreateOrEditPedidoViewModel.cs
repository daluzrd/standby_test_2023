using System.ComponentModel.DataAnnotations;
using Mvc.Models.Validations;

namespace Mvc.Models.Pedido;

public class CreateOrEditPedidoViewModel
{
    public Guid Id { get; set; }

    [DateGreaterThanToday]
    public DateTime Data { get; set; }
    public char Status { get; set; }

    [Required(ErrorMessage = "Cliente é obrigatório.")]
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
