using Domain.Models;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Produtos.Delete
{
    public class DeleteProdutoCommandHandler : ICommandHandler<DeleteProdutoCommand, DeleteProdutoCommandResult>
    {
        private readonly IRepository<Produto> _repository;

        public DeleteProdutoCommandHandler(IRepository<Produto> repository)
        {
            _repository = repository;
        }

        public async Task<DeleteProdutoCommandResult> Handle(DeleteProdutoCommand command, CancellationToken cancellationToken)
        {
            var validateResult = command.Validate();
            if (!validateResult.IsValid())
            {
                return new DeleteProdutoCommandResult(false, validateResult.GetErrorMessages());
            }

            var produto = await _repository.GetById(command.Id);
            if (produto == null)
            {
                return new DeleteProdutoCommandResult(false, "Produto não existe.");
            }
            
            _repository.Delete(produto);
            var result = await _repository.Commit();

            if (!result)
            {
                return new DeleteProdutoCommandResult(false, "Não foi possível excluir o produto.");
            }

            return new DeleteProdutoCommandResult(true, "Produto excluído com sucesso.");
        }
    }
}