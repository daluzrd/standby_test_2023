using SharedKernel.Commands;

namespace Application.UseCase.Clientes.Create;

public class CreateClienteCommandResult : CommandResult
{
    public Guid Id { get; private set; }

    public CreateClienteCommandResult(bool success, string message) : base(success, message) {}

    public CreateClienteCommandResult(bool success, List<string> message) : base(success, message) {}

    public CreateClienteCommandResult(bool success, string message, Guid id) : base(success, message)
    {
        Id = id;
    }
}