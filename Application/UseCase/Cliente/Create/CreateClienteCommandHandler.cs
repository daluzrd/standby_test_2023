using Domain.Models;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Clientes.Create;

public class CreateClienteCommandHandler : ICommandHandler<CreateClienteCommand, CreateClienteCommandResult>
{
    private readonly IRepository<Cliente> _repository;

    public CreateClienteCommandHandler(IRepository<Cliente> repository)
    {
        _repository = repository;
    }

    public async Task<CreateClienteCommandResult> Handle(CreateClienteCommand command, CancellationToken cancellationToken)
    {
        var validateResult = command.Validate();
        if (!validateResult.IsValid())
        {
            return new CreateClienteCommandResult(false, validateResult.GetErrorMessages());
        }

        var sameCnpjCliente = await _repository.Get(c => c.CpfCnpj == command.CpfCnpj);
        if (sameCnpjCliente.Any())
        {
            return new CreateClienteCommandResult(false, @"Já existe um cliente cadastrado este mesmo ""CpfCnpj"".");
        }

        var cliente = new Cliente(command.CpfCnpj, command.Nome);
        _repository.Add(cliente);

        var result = await _repository.Commit(cancellationToken);
        if (!result)
        {
            return new CreateClienteCommandResult(false, "Não foi possível criar o cliente.");
        }

        return new CreateClienteCommandResult(true, "Cliente criado com sucesso.", cliente.Id);
    }
}