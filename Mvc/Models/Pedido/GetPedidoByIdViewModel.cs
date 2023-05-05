namespace Mvc.Models.Pedido;

public class GetPedidoByIdViewModel
{
    public Guid Id { get; private set; }
    public DateTime Data { get; private set; }
    public char Status { get; private set; }
    public DateTime DataAtualizacao { get; private set; }
    public Guid ClienteId { get; private set; }

    public GetPedidoByIdViewModel(Guid id, DateTime data, char status, DateTime dataAtualizacao, Guid clienteId)
    {
        Id = id;
        Data = data;
        Status = status;
        DataAtualizacao = dataAtualizacao;
        ClienteId = clienteId;
    }
}
