using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Clientes.Create
{
    public class CreateClienteCommand : ICommand<CreateClienteCommandResult>
    {
        public string CpfCnpj { get; private set; }
        public string Nome { get; private set; }

        public CreateClienteCommand(string cpfCnpj, string nome)
        {
            CpfCnpj = cpfCnpj;
            Nome = nome;
        }

        public ResultValidator Validate()
        {
            var validator = new Validator<CreateClienteCommand>();

            validator.RuleFor(c => c.CpfCnpj)
                .NotEmpty()
                .WithMessage(@"O campo ""CpfCnpj"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""CpfCnpj"" é obrigatório.")
                .Must(c => c.Length == 11 || c.Length == 14)
                .WithMessage(@"O campo ""CpfCnpj"" está inválido.")
                .Matches(@"^\d+$")
                .WithMessage(@"O campo ""CpfCnpj"" aceita apenas caracteres numéricos.");

            validator.RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(@"O campo ""Nome"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Nome"" é obrigatório.")
                .Must(c => c.Length <= 100)
                .WithMessage(@"O campo nome deve possuir até 100 caracteres.");

            return new ResultValidator(validator.Validate(this));
        }
    }
}