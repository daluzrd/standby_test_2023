using Application.Dto.Pedido;
using Application.UseCase.Pedidos.Create;
using Application.UseCase.Pedidos.Update;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.MediatR;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;

        public PedidoController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] CreatePedidoDto createPedidoDto)
        {
            try
            {
                var pedidoProdutos = new List<PedidoProdutos>();
                foreach (var pedidoProdutoDto in createPedidoDto.PedidoProdutosDtos)
                {
                    pedidoProdutos.Add(new PedidoProdutos(pedidoProdutoDto.Quantidade, pedidoProdutoDto.ProdutoId));
                }

                var createPedidoCommand = new CreatePedidoCommand(
                    createPedidoDto.ClienteId,
                    createPedidoDto.Data,
                    pedidoProdutos);

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
}