using MediatR;
using SharedKernel.Commands;
using SharedKernel.Events;
using SharedKernel.Queries;

namespace SharedKernel.MediatR;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEvent<T>(T tEvent) where T : DomainEvent
    {
        await _mediator.Publish(tEvent);
    }

    public async Task<T> SendCommand<T>(ICommand<T> command) where T : CommandResult
    {
        return await _mediator.Send(command);
    }

    public async Task<T> SendQuery<T>(IQuerySingle<T> query) where T : QueryResult
    {
        return await _mediator.Send(query);
    }

    public async Task<T> SendQuery<T>(IQuery<T> query) where T : IEnumerable<QueryResult>
    {
        return await _mediator.Send(query);
    }
}