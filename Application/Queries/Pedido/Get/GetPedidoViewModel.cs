using SharedKernel.Queries;

namespace Application.Queries.Pedidos.Get;

public class GetPedidoViewModel : QueryResult
{
    public Guid Id { get; private set; }
    public DateTime Data { get; private set; }
    public char Status { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataAtualizacao { get; private set; }
    public string NomeCliente { get; private set; } = null!;

    public GetPedidoViewModel() {}

    public GetPedidoViewModel(
        Guid id, 
        DateTime data, 
        char status, 
        decimal valor, 
        DateTime dataAtualizacao)
    {
        Id = id;
        Data = data;
        Status = status;
        Valor = valor;
        DataAtualizacao = dataAtualizacao;
    }

    public GetPedidoViewModel(
        Guid id, 
        DateTime data, 
        char status, 
        decimal valor, 
        DateTime dataAtualizacao, 
        string nomeCliente) : this(id, data, status, valor, dataAtualizacao)
    {
        NomeCliente = nomeCliente;
    }

    public void AddNomeCliente(string nomeCliente)
    {
        NomeCliente = nomeCliente;
    }
}