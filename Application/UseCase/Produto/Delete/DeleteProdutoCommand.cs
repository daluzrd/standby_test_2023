using FluentValidation;
using SharedKernel.Commands;
using SharedKernel.Validators;

namespace Application.UseCase.Produtos.Delete
{
    public class DeleteProdutoCommand : ICommand<DeleteProdutoCommandResult>
    {
        public Guid Id { get; private set; }

        public DeleteProdutoCommand(Guid id)
        {
            Id = id;
        }

        public ResultValidator Validate()
        {            
            var validator = new Validator<DeleteProdutoCommand>();

            validator.RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage(@"O campo ""Id"" é obrigatório.")
                .NotNull()                
                .WithMessage(@"O campo ""Id"" é obrigatório.");

            return new ResultValidator(validator.Validate(this));
        }
    }
}