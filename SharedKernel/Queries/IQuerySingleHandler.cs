using MediatR;

namespace SharedKernel.Queries
{
    public interface IQuerySingleHandler<TInput, TResult> : IRequestHandler<TInput, TResult> where TInput : IQuerySingle<TResult> where TResult : QueryResult
    {

    }
}
