using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc.Controllers.Base;
using Mvc.DataService.Interface;
using Mvc.Models.Pedido;

namespace Mvc.Controllers;

[Route("[controller]")]
public class PedidoController : BaseController
{
    private readonly INotyfService _notyf;
    private readonly IPedidoService _pedidoService;
    private readonly IClienteService _clienteService;
    private readonly IPedidoItemService _pedidoItemService;

    public PedidoController(INotyfService notyf, IPedidoService pedidoService, IClienteService clienteService, IPedidoItemService pedidoItemService)
    {
        _notyf = notyf;
        _pedidoService = pedidoService;
        _clienteService = clienteService;
        _pedidoItemService = pedidoItemService;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index([FromQuery] string filter)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            ViewBag.Filter = filter ?? string.Empty;
            var pedidos = await _pedidoService.Get(token, filter);
            return View(pedidos);

        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return View();
        }
    }

    [HttpPost("Index")]
    public IActionResult FilteredIndex(string filter)
    {
        return Redirect($"~/Pedido/Index?filter={filter}");
    }

    [HttpGet("{id}/Item")]
    public async Task<IActionResult> PedidoItem([FromRoute] Guid id, [FromQuery] string filter)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            ViewBag.PedidoId = id;
            ViewBag.Filter = filter;

            var pedidoItems = await _pedidoItemService.GetPedidoItemByPedidoId(id, token, filter);
            return View(pedidoItems);

        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return View();
        }
    }

    [HttpPost("{id}/Item")]
    public IActionResult FilteredIndex(Guid id, string filter)
    {
        return Redirect($"~/Pedido/{id}/Item?filter={filter}");
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
            _notyf.Error(e.Message);
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
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            if (!ModelState.IsValid)
            {
                var clientes = await _clienteService.Get(token);
                ViewBag.Clientes = new SelectList(clientes, "Id", "Nome");

                return View(new GetPedidoByIdViewModel(
                    createOrEditPedidoViewModel.Id,
                    createOrEditPedidoViewModel.Data,
                    createOrEditPedidoViewModel.Status,
                    DateTime.Now,
                    createOrEditPedidoViewModel.ClienteId));
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            await _pedidoService.CreateOrEdit(createOrEditPedidoViewModel, token);
            if (createOrEditPedidoViewModel.Id != Guid.Empty)
            {
                _notyf.Success("Pedido cadastrado com sucesso.");
            }
            else
            {
                _notyf.Success("Pedido atualizado com sucesso.");
            }

            return Redirect("~/Pedido/Index");
        }
        catch (ArgumentException e)
        {
            _notyf.Warning(e.Message);
            return View(new GetPedidoByIdViewModel(
                    createOrEditPedidoViewModel.Id,
                    createOrEditPedidoViewModel.Data,
                    createOrEditPedidoViewModel.Status,
                    DateTime.Now,
                    createOrEditPedidoViewModel.ClienteId));
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return View(new GetPedidoByIdViewModel(
                    createOrEditPedidoViewModel.Id,
                    createOrEditPedidoViewModel.Data,
                    createOrEditPedidoViewModel.Status,
                    DateTime.Now,
                    createOrEditPedidoViewModel.ClienteId));
        }
    }

    [HttpPost("Close")]
    public async Task<IActionResult> Close(Guid id)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            await _pedidoService.Close(id, token);
            _notyf.Success("Pedido fechado com sucesso.");
        }
        catch (ArgumentException e)
        {
            _notyf.Warning(e.Message);
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
        }

        return RedirectToAction("Index");
    }

    [HttpPost("Cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            await _pedidoService.Cancel(id, token);
            _notyf.Success("Pedido cancelado com sucesso.");
        }
        catch (ArgumentException e)
        {
            _notyf.Warning(e.Message);
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
        }

        return RedirectToAction("Index");
    }
}