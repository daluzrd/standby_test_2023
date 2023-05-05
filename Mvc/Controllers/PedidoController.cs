using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc.Controllers.Base;
using Mvc.DataService.Interface;
using Mvc.Models;
using Mvc.Models.Cliente;
using Mvc.Models.Pedido;

namespace Mvc.Controllers;

[Route("[controller]")]
public class PedidoController : BaseController
{
    private readonly IPedidoService _pedidoService;
    private readonly IClienteService _clienteService;
    private readonly IPedidoItemService _pedidoItemService;
    public PedidoController(IPedidoService pedidoService, IClienteService clienteService, IPedidoItemService pedidoItemService, IProdutoService produtoService)
    {
        _pedidoService = pedidoService;
        _clienteService = clienteService;
        _pedidoItemService = pedidoItemService;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            var produtos = await _pedidoService.Get(token);
            return View(produtos);

        }
        catch (Exception e)
        {
            ViewBag.Error = e;
            return View();
        }
    }

    [HttpGet("{id}/Item")]
    public async Task<IActionResult> PedidoItem(Guid id)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            ViewBag.PedidoId = id;

            var pedidoItems = await _pedidoItemService.GetPedidoItemByPedidoId(id, token);
            return View(pedidoItems);

        }
        catch (Exception e)
        {
            ViewBag.Error = e;
            return View();
        }
    }

    [HttpGet("CreateOrEdit")]
    public async Task<IActionResult> CreateOrEdit(Guid id)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            var clientes = await _clienteService.Get(token);
            ViewBag.Clientes = new SelectList(clientes, "Id", "Nome");

            if (id != Guid.Empty)
            {
                return View(await _pedidoService.GetById(id, token));
            }

            return View(new GetPedidoByIdViewModel(Guid.Empty, DateTime.Now, 'A', DateTime.Now, Guid.Empty));
        }
        catch (Exception e)
        {
            return View();
        }
    }

    [HttpGet("CreateItem/{id}")]
    public IActionResult CreatePedidoItem([FromRoute] Guid id)
    {
        return Redirect("~/PedidoItem/Pedido/" + id + "/Create");
    }

    [HttpGet("EditItem/{id}")]
    public IActionResult EditPedidoItem(Guid id)
    {
        return Redirect("~/PedidoItem/" + id + "/Edit");
    }

    [HttpPost("CreateOrEdit")]
    public async Task<IActionResult> CreateOrEdit(CreateOrEditPedidoViewModel createOrEditPedidoViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(new GetPedidoByIdViewModel(
                    createOrEditPedidoViewModel.Id, 
                    createOrEditPedidoViewModel.Data, 
                    createOrEditPedidoViewModel.Status, 
                    DateTime.Now, 
                    createOrEditPedidoViewModel.ClienteId));
            }

            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }
            if (createOrEditPedidoViewModel == null)
            {
                return BadRequest(createOrEditPedidoViewModel);
            }

            await _pedidoService.CreateOrEdit(createOrEditPedidoViewModel, token);
            return Redirect("~/Pedido/Index");
        }
        catch (Exception e)
        {
            ViewBag.Error = e.Message;
            return View(new GetPedidoByIdViewModel(
                    createOrEditPedidoViewModel.Id,
                    createOrEditPedidoViewModel.Data,
                    createOrEditPedidoViewModel.Status,
                    DateTime.Now,
                    createOrEditPedidoViewModel.ClienteId));
        }
    }

    [HttpPatch("{id}/Close")]
    public async Task<IActionResult> Close([FromRoute] Guid id)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            await _pedidoService.Close(id, token);
            return NoContent();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("{id}/Cancel")]
    public async Task<IActionResult> Cancel([FromRoute] Guid id)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            await _pedidoService.Cancel(id, token);
            return NoContent();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}