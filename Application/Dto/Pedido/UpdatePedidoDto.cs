using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Pedido;

public record UpdatePedidoDto(
    [Required(ErrorMessage = "ClienteId é obrigatório.")] Guid clienteId, 
    [Required(ErrorMessage = "Data é obrigatória.")] DateTime data);