using SharedKernel.Commands;

namespace Application.UseCase.Produtos.Create;

public class CreateProdutoCommandResult : CommandResult
{
    public Guid Id { get; private set; }

    public CreateProdutoCommandResult(bool success, string message) : base(success, message) {}

    public CreateProdutoCommandResult(bool success, List<string> message) : base(success, message) {}

    public CreateProdutoCommandResult(bool success, string message, Guid id) : base(success, message)
    {
        Id = id;
    }
}