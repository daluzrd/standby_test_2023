using SharedKernel.Commands;

namespace Application.UseCase.Clientes.Delete
{
    public class DeleteClienteCommandResult : CommandResult
    {
        public DeleteClienteCommandResult(bool success, string message) : base(success, message) {}

        public DeleteClienteCommandResult(bool success, List<string> message) : base(success, message) {}
    }
}