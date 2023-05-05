using Mvc.Models;
using Mvc.Models.Pedido;
using Mvc.Models.PedidoItem;

namespace Mvc.DataService.Interface;

public interface IPedidoItemService
{
    Task<List<GetPedidoItemByPedidoIdViewModel>> GetPedidoItemByPedidoId(Guid pedidoId, string token);
    Task<GetPedidoItemByIdViewModel> GetPedidoItemById(Guid pedidoId, string token);
    Task<GenericResponseViewModel> CreateOrEdit(CreateOrEditPedidoItemViewModel model, string token);
    Task Delete(Guid id, string token);
}
