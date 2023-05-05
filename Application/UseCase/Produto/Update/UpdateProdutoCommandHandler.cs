using Domain.Models;
using SharedKernel.Commands;
using SharedKernel.Interfaces;

namespace Application.UseCase.Produtos.Update;

public class UpdateProdutoCommandHandler : ICommandHandler<UpdateProdutoCommand, UpdateProdutoCommandResult>
{
    private readonly IRepository<Produto> _repository;

    public UpdateProdutoCommandHandler(IRepository<Produto> repository)
    {
        _repository = repository;
    }

    public async Task<UpdateProdutoCommandResult> Handle(UpdateProdutoCommand command, CancellationToken cancellationToken)
    {
        var validateResult = command.Validate();
        if (!validateResult.IsValid())
        {
            return new UpdateProdutoCommandResult(false, validateResult.GetErrorMessages());
        }

        var sameCnpjProduto = await _repository.Get(c => c.Codigo == command.Codigo && c.Id != command.Id);
        if (sameCnpjProduto.Any())
        {
            return new UpdateProdutoCommandResult(false, @"Já existe um produto cadastrado com este mesmo ""codigo"".");
        }

        var produto = await _repository.GetById(command.Id);
        if (produto == null)
        {
            return new UpdateProdutoCommandResult(false, "Produto não existe.");
        }

        produto.ChangeProdutoData(command.Codigo, command.Descricao, command.QuantidadeEstoque, command.Valor);            
        _repository.Update(produto);

        var result = await _repository.Commit(cancellationToken);
        if (!result)
        {
            return new UpdateProdutoCommandResult(false, "Não foi possível criar o produto.");
        }

        return new UpdateProdutoCommandResult(true, "Produto atualizado com sucesso.", produto.Id);
    }
}