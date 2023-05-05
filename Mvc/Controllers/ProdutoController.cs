using Microsoft.AspNetCore.Mvc;
using Mvc.Controllers.Base;
using Mvc.DataService.Interface;
using Mvc.Models;
using Mvc.Models.Produto;

namespace Mvc.Controllers;

[Route("[controller]")]
public class ProdutoController : BaseController
{
    private readonly IProdutoService _produtoService;
    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var token = GetToken();
        if (string.IsNullOrWhiteSpace(token))
        {
            return Redirect("~/Account/Login");
        }

        var produtos = await _produtoService.Get(token);
        return View(produtos);
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
            ViewBag.Error = e;
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

            return Redirect("~/Produto/Index");
        }
        catch (Exception e)
        {
            ViewBag.Error = e.Message;
            return View(new ProdutoViewModel(
                createOrEditClienteViewModel.Id,
                createOrEditClienteViewModel.Codigo,
                createOrEditClienteViewModel.Descricao,
                createOrEditClienteViewModel.QuantidadeEstoque,
                createOrEditClienteViewModel.Valor));
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

            await _produtoService.Delete(id, token);

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}