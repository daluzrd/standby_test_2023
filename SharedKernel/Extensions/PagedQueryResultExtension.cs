using Microsoft.EntityFrameworkCore;
using SharedKernel.Queries;

namespace SharedKernel.Extensions
{
    public static class PagedQueryResultExtension
    {
        public static async Task<PagedQueryResult<T>> PaginatedAsync<T>(this IQueryable<T> source,int pageNumber,int pageSize) where T : class
        {
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;

            pageSize = (pageSize <= 0) ? 10 : pageSize;

            var firstItemPage = (pageNumber - 1) * pageSize;

            var items = await source
                              .AsNoTracking()
                              .Skip(firstItemPage)
                              .Take(pageSize).ToListAsync();

            var totalItems = await source.CountAsync();
            var pagedList = new PagedQueryResult<T>(items, totalItems, pageNumber, pageSize);      

            return pagedList;
        }

        public static PagedQueryResult<T> ToPaginatedList<T>(this List<T> source, int pageNumber, int pageSize) where T : class
        {
            var counter = source.Count;
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            var firstItemPage = (pageNumber - 1) * pageSize;
            var items = source.Skip(firstItemPage).Take(pageSize).ToList();
            return new PagedQueryResult<T>(items, counter, pageNumber, pageSize);
        }

        public static PagedQueryResult<T> ToPaginatedList<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            var counter = source.Count();
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            var firstItemPage = (pageNumber - 1) * pageSize;
            var items = source.Skip(firstItemPage).Take(pageSize).ToList();
            return new PagedQueryResult<T>(items, counter, pageNumber, pageSize);
        }
        public static PagedQueryResult<T> ToPaginatedList<T>(this ICollection<T> source, int pageNumber, int pageSize) where T : class
        {
            var counter = source.Count;
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            var firstItemPage = (pageNumber - 1) * pageSize;
            var items = source.Skip(firstItemPage).Take(pageSize).ToList();
            return new PagedQueryResult<T>(items, counter, pageNumber, pageSize);
        }
        public static PagedQueryResult<T> ToPaginatedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize) where T : class
        {
            var counter = source.Count();
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            var firstItemPage = (pageNumber - 1) * pageSize;
            var items = source.Skip(firstItemPage).Take(pageSize).ToList();
            return new PagedQueryResult<T>(items, counter, pageNumber, pageSize);
        }

    }
}
