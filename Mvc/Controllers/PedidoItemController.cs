using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc.Controllers.Base;
using Mvc.DataService.Interface;
using Mvc.Models.PedidoItem;

namespace Mvc.Controllers
{
    [Route("[controller]")]
    public class PedidoItemController : BaseController
    {
        private readonly IPedidoItemService _pedidoItemService;
        private readonly IProdutoService _produtoService;

        public PedidoItemController(IPedidoItemService pedidoItemService, IProdutoService produtoService)
        {
            _pedidoItemService = pedidoItemService;
            _produtoService = produtoService;
        }

        [HttpGet("Pedido/{id}/Create")]
        public async Task<IActionResult> Create(Guid id)
        {
            try
            {
                var token = GetToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return Redirect("~/Account/Login");
                }

                var produtos = await _produtoService.Get(token);
                ViewBag.Produtos = new SelectList(produtos, "Id", "Descricao");

                ModelState.Clear();
                return View("CreateOrEdit", new GetPedidoItemByIdViewModel(Guid.Empty, id, 0, Guid.Empty));
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return View("CreateOrEdit");
            }
        }

        [HttpGet("{id}/Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var token = GetToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return Redirect("~/Account/Login");
                }

                var produtos = await _produtoService.Get(token);
                ViewBag.Produtos = new SelectList(produtos, "Id", "Descricao");

                ModelState.Clear();
                return View("CreateOrEdit", await _pedidoItemService.GetPedidoItemById(id, token));
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return View("CreateOrEdit");
            }
        }

        [HttpPost("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPedidoItemViewModel createOrEditPedidoItemViewModel)
        {
            try
            {
                var token = GetToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return Redirect("~/Account/Login");
                }

                if (createOrEditPedidoItemViewModel == null)
                {
                    return BadRequest(createOrEditPedidoItemViewModel);
                }

                await _pedidoItemService.CreateOrEdit(createOrEditPedidoItemViewModel, token);

                return Redirect($"~/Pedido/{createOrEditPedidoItemViewModel.PedidoId}/Item");
            }
            catch (Exception)
            {
                return View(createOrEditPedidoItemViewModel);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var token = GetToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return Redirect("~/Account/Login");
                }

                await _pedidoItemService.Delete(id, token);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
