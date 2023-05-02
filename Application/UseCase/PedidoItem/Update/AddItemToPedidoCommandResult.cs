using SharedKernel.Commands;

namespace Application.UseCase.PedidoItems.Update
{
    public class AddItemToPedidoCommandResult : CommandResult
    {

        public AddItemToPedidoCommandResult(bool success, string message) : base(success, message) {}

        public AddItemToPedidoCommandResult(bool success, List<string> message) : base(success, message) {}
    }
}