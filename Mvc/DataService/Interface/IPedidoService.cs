using Mvc.Models;
using Mvc.Models.Pedido;

namespace Mvc.DataService.Interface;
public interface IPedidoService
{
    Task<IEnumerable<PedidoViewModel>> Get(string token);
    Task<GetPedidoByIdViewModel> GetById(Guid id, string token);
    Task<GenericResponseViewModel> CreateOrEdit(CreateOrEditPedidoViewModel model, string token);
    Task<GenericResponseViewModel> Close(Guid id, string token);
    Task<GenericResponseViewModel> Cancel(Guid id, string token);
}
