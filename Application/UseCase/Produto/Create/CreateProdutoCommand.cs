using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Produtos.Create;

public class CreateProdutoCommand : ICommand<CreateProdutoCommandResult>
{
    public string Codigo { get; private set; }
    public string Descricao { get; private set; }
    public int QuantidadeEstoque { get; private set; }
    public decimal Valor { get; private set; }

    public CreateProdutoCommand(string codigo, string descricao, int quantidadeEstoque, decimal valor)
    {
        Codigo = codigo;
        Descricao = descricao;
        QuantidadeEstoque = quantidadeEstoque;
        Valor = valor;
    }

    public ResultValidator Validate()
    {
        var validator = new Validator<CreateProdutoCommand>();

        validator.RuleFor(c => c.Codigo)
            .NotEmpty()
            .WithMessage("Código é obrigatório.")
            .NotNull()                
            .WithMessage("Código é obrigatório.")
            .Must(c => c.Length <= 20)
            .WithMessage("Código está inválido.");

        validator.RuleFor(c => c.Descricao)
            .NotEmpty()
            .WithMessage("Descrição é obrigatório.")
            .NotNull()                
            .WithMessage("Descrição é obrigatório.")
            .Must(c => c.Length <= 100)
            .WithMessage("Descrição está inválida.");

        validator.RuleFor(c => c.QuantidadeEstoque)
            .NotEmpty()
            .WithMessage("Estoque é obrigatório.")
            .NotNull()                
            .WithMessage("Estoque é obrigatório.");

        validator.RuleFor(c => c.Valor)
            .NotEmpty()
            .WithMessage("Valor é obrigatório.")
            .NotNull()                
            .WithMessage("Valor é obrigatório.")
            .PrecisionScale(18, 2, false)
            .WithMessage("Valor está inválido");

        return new ResultValidator(validator.Validate(this));
    }
}