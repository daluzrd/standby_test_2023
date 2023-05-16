using Application.Dto.PedidoItem;
using Application.Queries.PedidoItems.GetById;
using Application.UseCase.PedidoItems.Delete;
using Application.UseCase.PedidoItems.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.MediatR;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PedidoItemController : ControllerBase
{
    private readonly IMediatorHandler _mediator;
    public PedidoItemController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var getPedidoItemByIdQueryInput = new GetPedidoItemByIdQueryInput(id);
            var result = await _mediator.SendQuery(getPedidoItemByIdQueryInput);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] AddItemToPedidoCommand addItemToPedidoDto)
    {
        try
        {
            var addItemToPedidoCommand = new AddItemToPedidoCommand(
                addItemToPedidoDto.PedidoId,
                addItemToPedidoDto.ProdutoId,
                addItemToPedidoDto.Quantidade);

            var result = await _mediator.SendCommand(addItemToPedidoCommand);
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

    [HttpPatch("{id}/quantidade")]
    public async Task<IActionResult> UpdateQuantidade(
        Guid id, 
        [FromBody] UpdatePedidoItemQuantidadeDto updatePedidoItemQuantidadeDto)
    {
        try
        {
            var updatePedidoItemQuantidade = new UpdatePedidoItemQuantidadeCommand(
                id, 
                updatePedidoItemQuantidadeDto.Quantidade);

            var result = await _mediator.SendCommand(updatePedidoItemQuantidade);
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
            var deletePedidoItemCommand = new DeletePedidoItemCommand(id);

            var result = await _mediator.SendCommand(deletePedidoItemCommand);
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