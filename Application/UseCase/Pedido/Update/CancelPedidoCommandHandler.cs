using Domain.Models;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Pedidos.Update
{
    public class CancelPedidoCommandHandler : ICommandHandler<CancelPedidoCommand, ClosePedidoCommandResult>
    {
        private readonly IRepository<Pedido> _repository;

        public CancelPedidoCommandHandler(IRepository<Pedido> repository)
        {
            _repository = repository;
        }

        public async Task<ClosePedidoCommandResult> Handle(CancelPedidoCommand command, CancellationToken cancellationToken)
        {
            var validator = command.Validate();
            if (!validator.IsValid())
            {
                return new ClosePedidoCommandResult(false, validator.GetErrorMessages());
            }

            var pedido = await _repository.GetById(command.Id);
            if (pedido == null)
            {
                return new ClosePedidoCommandResult(false, "Pedido não existe.");
            }

            pedido.CancelPedido();
            var result = await _repository.Commit(cancellationToken);
            if (!result)
            {
                return new ClosePedidoCommandResult(false, "Não foi possível atualizar o pedido.");
            }

            return new ClosePedidoCommandResult(true, "Pedido atualizado com sucesso.");
        }
    }
}
