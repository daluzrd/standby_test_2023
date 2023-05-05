using MediatR;
using SharedKernel.Validators;

namespace SharedKernel.Commands;

public interface ICommand<out T>: IRequest<T> where T: CommandResult
{
    ResultValidator Validate();
}