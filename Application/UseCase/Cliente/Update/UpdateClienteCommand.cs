using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Clientes.Update
{
    public class UpdateClienteCommand : ICommand<UpdateClienteCommandResult>
    {
        public Guid Id { get; private set; }
        public string CpfCnpj { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        public UpdateClienteCommand(Guid id, string cpfCnpj, string nome, bool ativo)
        {
            Id = id;
            CpfCnpj = cpfCnpj;
            Nome = nome;
            Ativo = ativo;
        }

        public ResultValidator Validate()
        {
            var validator = new Validator<UpdateClienteCommand>();

            validator.RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage(@"O campo ""Id"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Id"" é obrigatório.");

            validator.RuleFor(c => c.Ativo)
                .NotEmpty()
                .WithMessage(@"O campo ""Ativo"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Ativo"" é obrigatório.");

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