using Mvc.Models.Validations;

namespace Mvc.Models.PedidoItem;

public class CreateOrEditPedidoItemViewModel
{
    public Guid Id { get; set; }
    public Guid PedidoId { get; set; }
    public Guid ProdutoId { get; set; }

    [IntGreaterThanZero]
    public int Quantidade { get; set; }

    public CreateOrEditPedidoItemViewModel() { }

    public CreateOrEditPedidoItemViewModel(Guid id, Guid pedidoId, int quantidade, Guid produtoId)
    {
        Id = id;
        PedidoId = pedidoId;
        Quantidade = quantidade;
        ProdutoId = produtoId;
    }
}
