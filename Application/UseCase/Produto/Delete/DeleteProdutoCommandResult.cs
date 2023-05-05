using SharedKernel.Commands;

namespace Application.UseCase.Produtos.Delete;

public class DeleteProdutoCommandResult : CommandResult
{
    public DeleteProdutoCommandResult(bool success, string message) : base(success, message) {}

    public DeleteProdutoCommandResult(bool success, List<string> message) : base(success, message) {}
}