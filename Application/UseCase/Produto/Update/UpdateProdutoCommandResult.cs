using SharedKernel.Commands;

namespace Application.UseCase.Produtos.Update
{
    public class UpdateProdutoCommandResult : CommandResult
    {
        public Guid Id { get; private set; }

        public UpdateProdutoCommandResult(bool success, string message) : base(success, message) {}

        public UpdateProdutoCommandResult(bool success, List<string> message) : base(success, message) {}

        public UpdateProdutoCommandResult(bool success, string message, Guid id) : base(success, message)
        {
            Id = id;
        }
    }
}