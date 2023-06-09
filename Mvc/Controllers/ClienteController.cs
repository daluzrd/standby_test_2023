using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Mvc.Controllers.Base;
using Mvc.DataService.Interface;
using Mvc.Models.Cliente;

namespace Mvc.Controllers;

[Route("[controller]")]
public class ClienteController : BaseController
{
    private readonly INotyfService _notyf;
    private readonly IClienteService _clienteService;

    public ClienteController(INotyfService notyf, IClienteService clienteService)
    {
        _notyf = notyf;
        _clienteService = clienteService;
    }

    [HttpGet("Index")]
    public IActionResult Index()
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            return View();
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return View();
        }
    }

    [HttpGet("Data")]
    public async Task<IActionResult> GetData([FromQuery] IDictionary<string, string> search, int draw)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            var clientes = await _clienteService.Get(token, search["value"]);
            clientes.AddDraw(draw);
            return Ok(clientes);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }



    [HttpPost("Index")]
    public IActionResult IndexFiltered(string filter)
    {
        return Redirect($"~/Cliente/Index?filter={filter}");
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

            if (id != Guid.Empty)
            {
                return View(await _clienteService.GetById(id, token));
            }

            return View(new ClienteByIdViewModel(Guid.Empty, string.Empty, string.Empty, true));
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return View();
        }
    }

    [HttpPost("CreateOrEdit")]
    public async Task<IActionResult> CreateOrEdit(CreateOrEditClienteViewModel createOrEditClienteViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(new ClienteByIdViewModel(
                    createOrEditClienteViewModel.Id,
                    createOrEditClienteViewModel.Nome,
                    createOrEditClienteViewModel.CpfCnpj,
                    createOrEditClienteViewModel.Ativo));
            }

            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            if (createOrEditClienteViewModel == null)
            {
                return BadRequest(createOrEditClienteViewModel);
            }

            await _clienteService.CreateOrEdit(createOrEditClienteViewModel, token);
            if (createOrEditClienteViewModel.Id != Guid.Empty)
            {
                _notyf.Success("Cliente atualizado com sucesso.");
            }
            else
            {
                _notyf.Success("Cliente cadastrado com sucesso.");
            }

            return Redirect("~/Cliente/Index");
        }
        catch (ArgumentException e)
        {
            _notyf.Warning(e.Message);
            return View(new ClienteByIdViewModel(
                createOrEditClienteViewModel.Id,
                createOrEditClienteViewModel.Nome,
                createOrEditClienteViewModel.CpfCnpj,
                createOrEditClienteViewModel.Ativo));
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return View(new ClienteByIdViewModel(
                createOrEditClienteViewModel.Id,
                createOrEditClienteViewModel.Nome,
                createOrEditClienteViewModel.CpfCnpj,
                createOrEditClienteViewModel.Ativo));
        }
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            await _clienteService.Delete(id, token);
            _notyf.Success("Cliente exclu�do com sucesso.");
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