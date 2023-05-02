using Application.Queries.Produtos.GetById;
using SharedKernel.Queries;

namespace Application.Queries.Produtos.Get
{
    public class GetProdutoQueryInput : IQuery<IEnumerable<GetProdutoByIdViewModel>>
    {
        public string? Codigo { get; private set; }
        public string? Descricao { get; private set; }
        public int? QuantidadeEstoque { get; private set; }
        public decimal? Valor { get; private set; }
        public string? OrderBy { get; private set; }
        public bool? OrderAsc { get; private set; }

        public GetProdutoQueryInput(
            string? codigo, 
            string? descricao, 
            int? quantidadeEstoque, 
            decimal? valor, 
            string? orderBy, 
            bool? orderAsc)
        {
            Codigo = codigo;
            Descricao = descricao;
            QuantidadeEstoque = quantidadeEstoque;
            Valor = valor;
            OrderBy = orderBy;
            OrderAsc = orderAsc;
        }
    }

}