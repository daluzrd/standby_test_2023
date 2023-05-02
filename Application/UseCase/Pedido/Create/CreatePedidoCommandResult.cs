using SharedKernel.Commands;

namespace Application.UseCase.Pedidos.Create
{
    public class CreatePedidoCommandResult : CommandResult
    {
        public Guid Id { get; private set; }

        public CreatePedidoCommandResult(bool success, string message) : base(success, message) {}

        public CreatePedidoCommandResult(bool success, List<string> message) : base(success, message) {}

        public CreatePedidoCommandResult(bool success, string message, Guid id) : base(success, message)
        {
            Id = id;
        }
    }
}