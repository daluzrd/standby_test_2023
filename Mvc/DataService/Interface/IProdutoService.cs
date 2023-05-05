using Mvc.Models;
using Mvc.Models.Produto;

namespace Mvc.DataService.Interface;
public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> Get(string token);
    Task<ProdutoViewModel> GetById(Guid id, string token);
    Task<GenericResponseViewModel> CreateOrEdit(CreateOrEditProdutoViewModel model, string token);
    Task Delete(Guid id, string token);
}
