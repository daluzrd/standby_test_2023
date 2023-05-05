using Domain.Models;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.GetById;

public class GetProdutoByIdViewModel : QueryResult
{
    public Guid Id { get; private set; }
    public string Codigo { get; private set; }
    public string Descricao { get; private set; }
    public int QuantidadeEstoque { get; private set; }
    public decimal Valor { get; private set; }
    public List<PedidoItem> PedidoItens;

    public GetProdutoByIdViewModel() {}

    public GetProdutoByIdViewModel(
        Guid id, 
        string codigo, 
        string descricao, 
        int quantidadeEstoque, 
        decimal valor)
    {
        Id = id;
        Codigo = codigo;
        Descricao = descricao;
        QuantidadeEstoque = quantidadeEstoque;
        Valor = valor;
        PedidoItens = new();
    }

    public GetProdutoByIdViewModel(
        Guid id, 
        string codigo, 
        string descricao, 
        int quantidadeEstoque, 
        decimal valor, 
        List<PedidoItem> pedidoItens) : this(id, codigo, descricao, quantidadeEstoque, valor)
    {
        PedidoItens = pedidoItens;
    }
}