using SharedKernel.Commands;
using SharedKernel.Events;
using SharedKernel.Queries;

namespace SharedKernel.MediatR
{
    public interface IMediatorHandler
    {
        Task<T> SendCommand<T>(ICommand<T> command) where T: CommandResult;
        Task PublishEvent<T>(T tEvent) where T : DomainEvent;
        Task<T> SendQuery<T>(IQuerySingle<T> query) where T : QueryResult;
        Task<T> SendQuery<T>(IQuery<T> query) where T : IEnumerable<QueryResult>;
    }
}