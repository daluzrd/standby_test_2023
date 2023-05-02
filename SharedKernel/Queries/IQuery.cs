using MediatR;

namespace SharedKernel.Queries
{
    public interface IQuery<out T> : IRequest<T> where T : IEnumerable<QueryResult>
    {
        
    }
}
