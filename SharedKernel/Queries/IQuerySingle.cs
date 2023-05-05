using MediatR;

namespace SharedKernel.Queries;

public interface IQuerySingle<out T> : IRequest<T> where T : QueryResult
{

}
