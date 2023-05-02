using Application.Dto.PedidoItem;
using Application.UseCase.PedidoItems.Update;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.MediatR;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoItemController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;
        public PedidoItemController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] AddItemToPedidoCommand addItemToPedidoDto)
        {
            try
            {
                var createPedidoCommand = new AddItemToPedidoCommand(
                    addItemToPedidoDto.PedidoId,
                    addItemToPedidoDto.ProdutoId,
                    addItemToPedidoDto.Quantidade);

                var result = await _mediator.SendCommand(createPedidoCommand);
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
    }
}