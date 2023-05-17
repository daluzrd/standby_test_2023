namespace SharedKernel.Interfaces;

public interface IReadRepository<T> where T : class
{
    Task<IEnumerable<T>> ExecuteQueryAsync(string sql, object? param = null);
    Task<IEnumerable<TReturn>> ExecuteQueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn, object? param = null);
    Task<IEnumerable<TReturn>> ExecuteQueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn, object? param = null);
    Task<T> ExecuteQueryFirstOrDefaultAsync(string sql, object? param = null);
    string BuildQueryFilters(string query, List<string>? textVariables = null, List<string>? numberVariables = null, List<string>? dateVariables = null);
}