using Application.Dto.Produtos;
using Application.Queries.Produtos.Get;
using Application.Queries.Produtos.GetById;
using Application.UseCase.Produtos.Create;
using Application.UseCase.Produtos.Delete;
using Application.UseCase.Produtos.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.MediatR;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IMediatorHandler _mediator;

    public ProdutoController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<IActionResult> Get([FromQuery] string filter)
    {
        try
        {
            var getProdutoQueryInput = new GetProdutoQueryInput(filter);
            var result = await _mediator.SendQuery(getProdutoQueryInput);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var getProdutoByIdQueryInput = new GetProdutoByIdQueryInput(id);
            var result = await _mediator.SendQuery(getProdutoByIdQueryInput);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] CreateProdutoDto createProdutoDto)
    {
        try
        {
            var createProdutoCommand = new CreateProdutoCommand(
                createProdutoDto.Codigo, 
                createProdutoDto.Descricao, 
                createProdutoDto.QuantidadeEstoque, 
                createProdutoDto.Valor);

            var result = await _mediator.SendCommand(createProdutoCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProdutoDto updateProdutoDto)
    {
        try
        {
            var updateProdutoCommand = new UpdateProdutoCommand(
                id,
                updateProdutoDto.Codigo, 
                updateProdutoDto.Descricao, 
                updateProdutoDto.QuantidadeEstoque, 
                updateProdutoDto.Valor);

            var result = await _mediator.SendCommand(updateProdutoCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var deleteProdutoCommand = new DeleteProdutoCommand(id);

            var result = await _mediator.SendCommand(deleteProdutoCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}