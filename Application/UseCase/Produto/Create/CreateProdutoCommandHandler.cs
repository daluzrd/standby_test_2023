using Domain.Models;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Produtos.Create;

public class CreateProdutoCommandHandler : ICommandHandler<CreateProdutoCommand, CreateProdutoCommandResult>
{
    private readonly IRepository<Produto> _repository;

    public CreateProdutoCommandHandler(IRepository<Produto> repository)
    {
        _repository = repository;
    }

    public async Task<CreateProdutoCommandResult> Handle(CreateProdutoCommand command, CancellationToken cancellationToken)
    {
        var validateResult = command.Validate();
        if (!validateResult.IsValid())
        {
            return new CreateProdutoCommandResult(false, validateResult.GetErrorMessages());
        }

        var sameCodigo = await _repository.Get(c => c.Codigo == command.Codigo);
        if (sameCodigo.Any())
        {
            return new CreateProdutoCommandResult(false, @"Já existe um produto cadastrado com este código.");
        }

        var produto = new Produto(command.Codigo, command.Descricao, command.QuantidadeEstoque, command.Valor);
        _repository.Add(produto);

        var result = await _repository.Commit(cancellationToken);
        if (!result)
        {
            return new CreateProdutoCommandResult(false, "Não foi possível criar o cliente.");
        }

        return new CreateProdutoCommandResult(true, "Produto criado com sucesso.", produto.Id);
    }
}