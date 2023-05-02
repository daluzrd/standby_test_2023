using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Clientes.Delete
{
    public class DeleteClienteCommand : ICommand<DeleteClienteCommandResult>
    {
        public Guid Id { get; private set; }

        public DeleteClienteCommand(Guid id)
        {
            Id = id;
        }

        public ResultValidator Validate()
        {            
            var validator = new Validator<DeleteClienteCommand>();

            validator.RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage(@"O campo ""Id"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Id"" é obrigatório.");

            return new ResultValidator(validator.Validate(this));
        }
    }
}