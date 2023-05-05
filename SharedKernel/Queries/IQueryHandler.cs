using MediatR;

namespace SharedKernel.Queries;

public interface IQueryHandler<TInput, TResult> : IRequestHandler<TInput, TResult> where TInput : IQuery<TResult> where TResult : IEnumerable<QueryResult>
{
    
}
