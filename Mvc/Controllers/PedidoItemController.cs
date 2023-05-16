using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc.Controllers.Base;
using Mvc.DataService.Interface;
using Mvc.Models.PedidoItem;

namespace Mvc.Controllers;

[Route("[controller]")]
public class PedidoItemController : BaseController
{
    private readonly INotyfService _notyf;
    private readonly IPedidoItemService _pedidoItemService;
    private readonly IProdutoService _produtoService;

    public PedidoItemController(INotyfService notyf, IPedidoItemService pedidoItemService, IProdutoService produtoService)
    {
        _notyf = notyf;
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
            _notyf.Error(e.Message);
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
            _notyf.Error(e.Message);
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

            if (!ModelState.IsValid)
            {
                var produtos = await _produtoService.Get(token);
                ViewBag.Produtos = new SelectList(produtos, "Id", "Descricao");

                return View(new GetPedidoItemByIdViewModel(
                    createOrEditPedidoItemViewModel.Id,
                    createOrEditPedidoItemViewModel.PedidoId,
                    createOrEditPedidoItemViewModel.Quantidade,
                    createOrEditPedidoItemViewModel.ProdutoId));
            }

            if (createOrEditPedidoItemViewModel == null)
            {
                return BadRequest(createOrEditPedidoItemViewModel);
            }

            await _pedidoItemService.CreateOrEdit(createOrEditPedidoItemViewModel, token);

            if (createOrEditPedidoItemViewModel.Id != Guid.Empty)
            {
                _notyf.Success("Item do pedido atualizado com sucesso.");
            }
            else
            {
                _notyf.Success("Item do pedido cadastrado com sucesso.");
            }

            return Redirect($"~/Pedido/{createOrEditPedidoItemViewModel.PedidoId}/Item");
        }
        catch (ArgumentException e)
        {
            _notyf.Warning(e.Message);
            return View(createOrEditPedidoItemViewModel);
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
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
