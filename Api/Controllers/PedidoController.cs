using Application.Dto.Pedido;
using Application.Queries.PedidoItems.GetByPedidoId;
using Application.Queries.Pedidos.Get;
using Application.Queries.Pedidos.GetById;
using Application.UseCase.Pedidos.Create;
using Application.UseCase.Pedidos.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.MediatR;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IMediatorHandler _mediator;

    public PedidoController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        try
        {
            var getPedidoQueryInput = new GetPedidoQueryInput();
            var result = await _mediator.SendQuery(getPedidoQueryInput);

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
            var getPedidoByIdQueryInput = new GetPedidoByIdQueryInput(id);
            var result = await _mediator.SendQuery(getPedidoByIdQueryInput);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}/Item")]
    public async Task<IActionResult> GetPedidoItemByPedidoId(Guid id)
    {
        try
        {
            var request = new GetPedidoItemByPedidoIdQueryInput(id);
            var result = await _mediator.SendQuery(request);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] CreatePedidoDto createPedidoDto)
    {
        try
        {
            var createPedidoCommand = new CreatePedidoCommand(
                createPedidoDto.ClienteId,
                createPedidoDto.Data);

            var result = await _mediator.SendCommand(createPedidoCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePedido(Guid id, UpdatePedidoDto updatePedidoDto)
    {
        try
        {
            var updatePedidoCommand = new UpdatePedidoCommand(
                id, 
                updatePedidoDto.clienteId, 
                updatePedidoDto.data);
            
            var result = await _mediator.SendCommand(updatePedidoCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("{id}/close")]
    public async Task<IActionResult> ClosePedido(Guid id)
    {
        try
        {
            var closePedidoCommand = new ClosePedidoCommand(id);

            var result = await _mediator.SendCommand(closePedidoCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> CancelPedido(Guid id)
    {
        try
        {
            var cancelPedidoCommand = new CancelPedidoCommand(id);

            var result = await _mediator.SendCommand(cancelPedidoCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}