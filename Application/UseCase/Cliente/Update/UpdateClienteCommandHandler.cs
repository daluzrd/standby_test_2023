using Domain.Models;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Clientes.Update
{
    public class UpdateClienteCommandHandler : ICommandHandler<UpdateClienteCommand, UpdateClienteCommandResult>
    {
        private readonly IRepository<Cliente> _repository;

        public UpdateClienteCommandHandler(IRepository<Cliente> repository)
        {
            _repository = repository;
        }

        public async Task<UpdateClienteCommandResult> Handle(UpdateClienteCommand command, CancellationToken cancellationToken)
        {
            var validateResult = command.Validate();
            if (!validateResult.IsValid())
            {
                return new UpdateClienteCommandResult(false, validateResult.GetErrorMessages());
            }

            var sameCnpjCliente = await _repository.Get(c => c.CpfCnpj == command.CpfCnpj && c.Id != command.Id);
            if (sameCnpjCliente.Any())
            {
                return new UpdateClienteCommandResult(false, @"Já existe um cliente cadastrado com um mesmo ""CpfCnpj"" cadastrado.");
            }

            var cliente = await _repository.GetById(command.Id);
            if (cliente == null)
            {
                return new UpdateClienteCommandResult(false, "Cliente não existe.");
            }

            cliente.ChangeClienteData(command.CpfCnpj, command.Nome, command.Ativo);            
            _repository.Update(cliente);

            var result = await _repository.Commit(cancellationToken);
            if (!result)
            {
                return new UpdateClienteCommandResult(false, "Não foi possível criar o cliente.");
            }

            return new UpdateClienteCommandResult(true, "Cliente criado com sucesso.", cliente.Id);
        }
    }
}