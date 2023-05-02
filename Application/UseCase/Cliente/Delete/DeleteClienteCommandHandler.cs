using Domain.Models;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Clientes.Delete
{
    public class DeleteClienteCommandHandler : ICommandHandler<DeleteClienteCommand, DeleteClienteCommandResult>
    {
        private readonly IRepository<Cliente> _repository;

        public DeleteClienteCommandHandler(IRepository<Cliente> repository)
        {
            _repository = repository;
        }

        public async Task<DeleteClienteCommandResult> Handle(DeleteClienteCommand command, CancellationToken cancellationToken)
        {
            var validateResult = command.Validate();
            if (!validateResult.IsValid())
            {
                return new DeleteClienteCommandResult(false, validateResult.GetErrorMessages());
            }

            var cliente = await _repository.GetById(command.Id);
            if (cliente == null)
            {
                return new DeleteClienteCommandResult(false, "Cliente não existe.");
            }
            
            _repository.Delete(cliente);
            var result = await _repository.Commit();

            if (!result)
            {
                return new DeleteClienteCommandResult(false, "Não foi possível excluir o cliente.");
            }

            return new DeleteClienteCommandResult(true, "Cliente excluído com sucesso.");
        }
    }
}