using MediatR;

namespace SharedKernel.Commands
{
    public interface ICommandHandler<in TInput, TResponse> : IRequestHandler<TInput, TResponse> where TInput : ICommand<TResponse> where TResponse : CommandResult
    {

    }
}