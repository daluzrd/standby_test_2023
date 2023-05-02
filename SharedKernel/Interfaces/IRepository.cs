using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SharedKernel.Interfaces
{
    public interface IRepository<T> where T : class, IAggregateRoot
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<T?> GetById(Guid id);
        Task<List<T>> Get(Expression<Func<T, bool>> expression = null!, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null!);
        Task<bool> Commit(CancellationToken cancellationToken = default);
        Task<T?> FirstAsync(Expression<System.Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null!);

    }
}