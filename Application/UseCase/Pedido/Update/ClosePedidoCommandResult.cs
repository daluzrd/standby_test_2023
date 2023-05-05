using SharedKernel.Commands;

namespace Application.UseCase.Pedidos.Update;

public class ClosePedidoCommandResult : CommandResult
{
    public ClosePedidoCommandResult(bool success, string message) : base(success, message) { }

    public ClosePedidoCommandResult(bool success, List<string> message) : base(success, message) { }
}