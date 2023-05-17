using Mvc.Models;
using Mvc.Models.Cliente;

namespace Mvc.DataService.Interface;

public interface IClienteService
{
    Task<IEnumerable<ClienteViewModel>> Get(string token, string? filter = null);
    Task<ClienteViewModel> GetById(Guid id, string token);
    Task<GenericResponseViewModel> CreateOrEdit(CreateOrEditClienteViewModel model, string token);
    Task Delete(Guid id, string token);
}