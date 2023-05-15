using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Pedidos.Update;

public class UpdatePedidoCommandHandler : ICommandHandler<UpdatePedidoCommand, ClosePedidoCommandResult>
{
    private readonly IRepository<Pedido> _repository;
    private readonly IRepository<Cliente> _clienteRepository;

    public UpdatePedidoCommandHandler(IRepository<Pedido> repository, IRepository<Cliente> clienteRepository)
    {
        _repository = repository;
        _clienteRepository = clienteRepository;
    }

    public async Task<ClosePedidoCommandResult> Handle(UpdatePedidoCommand command, CancellationToken cancellationToken)
    {
        var resultValidator = command.Validate();
        if (!resultValidator.IsValid())
        {
            return new ClosePedidoCommandResult(false, resultValidator.GetErrorMessages());
        }

        var pedido = (await _repository.Get(
            p => p.Id == command.Id, 
            include: pedido => pedido.Include(p => p.Cliente))).FirstOrDefault();
        if (pedido == null)
        {
            return new ClosePedidoCommandResult(false, "Pedido não existe.");
        }

        if (command.ClienteId != pedido.Cliente.Id)
        {
            var cliente = await _clienteRepository.GetById(command.ClienteId);
            if (cliente == null)
            {
                return new ClosePedidoCommandResult(false, "Cliente não existe.");
            }

            pedido.UpdateCliente(cliente);
        }

        pedido.UpdateData(command.Data);
        pedido.UpdateDataAtualizacao();
        
        var result = await _repository.Commit(cancellationToken);
        if (!result)
        {
            return new ClosePedidoCommandResult(false, "Não foi possível atualizar o pedido.");
        }

        return new ClosePedidoCommandResult(true, "Pedido atualizado com sucesso.");
    }
}