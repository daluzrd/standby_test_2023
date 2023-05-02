using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Produtos.Create
{
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
                .WithMessage(@"O campo ""Codigo"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Codigo"" é obrigatório.")
                .Must(c => c.Length <= 20)
                .WithMessage(@"O campo ""Codigo"" está inválido.");

            validator.RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage(@"O campo ""Descricao"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Descricao"" é obrigatório.")
                .Must(c => c.Length <= 100)
                .WithMessage(@"O campo ""Descricao"" está inválido.");

            validator.RuleFor(c => c.QuantidadeEstoque)
                .NotEmpty()
                .WithMessage(@"O campo ""QuantidadeEstoque"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""QuantidadeEstoque"" é obrigatório.");

            validator.RuleFor(c => c.Valor)
                .NotEmpty()
                .WithMessage(@"O campo ""Valor"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Valor"" é obrigatório.")
                .PrecisionScale(18, 2, false)
                .WithMessage(@"O campo ""Valor"" está inválido");

            return new ResultValidator(validator.Validate(this));
        }
    }
}