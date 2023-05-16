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
    public async Task<IActionResult> Index()
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            var clientes = await _clienteService.Get(token);
            return View(clientes);

        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
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

            if (id != Guid.Empty)
            {
                return View(await _clienteService.GetById(id, token));
            }

            return View(new ClienteViewModel(Guid.Empty, string.Empty, string.Empty, true));
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
                return View(new ClienteViewModel(
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
            return View(new ClienteViewModel(
                createOrEditClienteViewModel.Id,
                createOrEditClienteViewModel.Nome,
                createOrEditClienteViewModel.CpfCnpj,
                createOrEditClienteViewModel.Ativo));
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return View(new ClienteViewModel(
                createOrEditClienteViewModel.Id,
                createOrEditClienteViewModel.Nome,
                createOrEditClienteViewModel.CpfCnpj,
                createOrEditClienteViewModel.Ativo));
        }
    }

    [HttpDelete()]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            await _clienteService.Delete(id, token);
            _notyf.Success("Cliente excluído com sucesso.");

            return NoContent();
        }
        catch (ArgumentException e)
        {
            _notyf.Warning(e.Message);
            return StatusCode(400, e.Message);
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return StatusCode(500, e.Message);
        }
    }
}