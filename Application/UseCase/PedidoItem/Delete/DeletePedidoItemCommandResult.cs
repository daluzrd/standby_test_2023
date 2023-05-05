using SharedKernel.Commands;

namespace Application.UseCase.PedidoItems.Delete;

public class DeletePedidoItemCommandResult : CommandResult
{
    public DeletePedidoItemCommandResult(bool success, string message) : base(success, message) { }

    public DeletePedidoItemCommandResult(bool success, List<string> message) : base(success, message) { }
}