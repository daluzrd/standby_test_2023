using Application.Dto.Cliente;
using Application.Queries.Clientes.Get;
using Application.Queries.Clientes.GetById;
using Application.UseCase.Clientes.Create;
using Application.UseCase.Clientes.Delete;
using Application.UseCase.Clientes.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.MediatR;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IMediatorHandler _mediator;
    public ClienteController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<IActionResult> Get([FromQuery] string filter, string orderBy = "id", bool orderAsc = true)
    {
        try
        {
            var getClienteQueryInput = new GetClienteQueryInput(filter, orderBy, orderAsc);
            var result = await _mediator.SendQuery(getClienteQueryInput);

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
            var getClienteQueryInput = new GetClienteByIdQueryInput(id);
            var result = await _mediator.SendQuery(getClienteQueryInput);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] CreateClienteDto createCustomerDto)
    {
        try
        {
            var createClienteCommand = new CreateClienteCommand(createCustomerDto.CpfCnpj, createCustomerDto.Nome);

            var result = await _mediator.SendCommand(createClienteCommand);
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
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateClienteDto updateClienteDto)
    {
        try
        {
            var updateClienteCommand = new UpdateClienteCommand(
                id,
                updateClienteDto.CpfCnpj,
                updateClienteDto.Nome,
                updateClienteDto.Ativo);


            var result = await _mediator.SendCommand(updateClienteCommand);
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
            var deleteClienteCommand = new DeleteClienteCommand(id);

            var result = await _mediator.SendCommand(deleteClienteCommand);
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