using SharedKernel.Commands;

namespace Application.UseCase.Clientes.Update;

public class UpdateClienteCommandResult : CommandResult
{
    public Guid Id { get; private set; }

    public UpdateClienteCommandResult(bool success, string message) : base(success, message) {}

    public UpdateClienteCommandResult(bool success, List<string> message) : base(success, message) {}

    public UpdateClienteCommandResult(bool success, string message, Guid id) : base(success, message)
    {
        Id = id;
    }
}