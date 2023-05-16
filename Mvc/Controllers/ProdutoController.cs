using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Mvc.Controllers.Base;
using Mvc.DataService.Interface;
using Mvc.Models.Produto;

namespace Mvc.Controllers;

[Route("[controller]")]
public class ProdutoController : BaseController
{
    private readonly INotyfService _notyf;
    private readonly IProdutoService _produtoService;

    public ProdutoController(INotyfService notyf, IProdutoService produtoService)
    {
        _notyf = notyf;
        _produtoService = produtoService;
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

            var produtos = await _produtoService.Get(token);
            return View(produtos);
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
                return View(await _produtoService.GetById(id, token));
            }

            return View(new ProdutoViewModel(Guid.Empty, string.Empty, string.Empty, 0, 0));
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return View();
        }
    }

    [HttpPost("CreateOrEdit")]
    public async Task<IActionResult> CreateOrEdit(CreateOrEditProdutoViewModel createOrEditClienteViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(new ProdutoViewModel(
                    createOrEditClienteViewModel.Id,
                    createOrEditClienteViewModel.Codigo,
                    createOrEditClienteViewModel.Descricao,
                    createOrEditClienteViewModel.QuantidadeEstoque,
                    createOrEditClienteViewModel.Valor));
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

            await _produtoService.CreateOrEdit(createOrEditClienteViewModel, token);
            if (createOrEditClienteViewModel.Id != Guid.Empty)
            {
                _notyf.Success("Produto atualizado com sucesso.");
            }
            else
            {
                _notyf.Success("Produto cadastrado com sucesso.");
            }

            return Redirect("~/Produto/Index");
        }
        catch (ArgumentException e)
        {
            _notyf.Warning(e.Message);
            return View(new ProdutoViewModel(
                createOrEditClienteViewModel.Id,
                createOrEditClienteViewModel.Codigo,
                createOrEditClienteViewModel.Descricao,
                createOrEditClienteViewModel.QuantidadeEstoque,
                createOrEditClienteViewModel.Valor));
        }
        catch (Exception e)
        {
            _notyf.Error(e.Message);
            return View(new ProdutoViewModel(
                createOrEditClienteViewModel.Id,
                createOrEditClienteViewModel.Codigo,
                createOrEditClienteViewModel.Descricao,
                createOrEditClienteViewModel.QuantidadeEstoque,
                createOrEditClienteViewModel.Valor));
        }
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        try
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            await _produtoService.Delete(id, token);

            _notyf.Success("Produto excluído com sucesso.");
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