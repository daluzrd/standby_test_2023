using Dapper;
using SharedKernel.Interfaces;

namespace Infrastructure.Data.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class
{
    private readonly DapperContext _dapperContext;

    public ReadRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<T> ExecuteQueryFirstOrDefaultAsync(string sql, object? param = null)
    {
        var connection = _dapperContext.GetConection();
        var queryData = await connection.QueryFirstOrDefaultAsync<T>(sql, param);

        return queryData;

    }

    public async Task<IEnumerable<TReturn>> ExecuteQueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn, object? param = null)
    {
        var connection = _dapperContext.GetConection();
        IEnumerable<TReturn> queryData = await connection.QueryAsync<TFirst, TSecond, TReturn>(sql, map, param, splitOn: splitOn);

        return queryData;
    }

    public async Task<IEnumerable<TReturn>> ExecuteQueryFirstOrDefaultAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn, object? param = null)
    {
        var connection = _dapperContext.GetConection();
        IEnumerable<TReturn> queryData = await connection.QueryAsync<TFirst, TSecond, TReturn>(sql, map, param, splitOn: splitOn);

        return queryData;
    }

    public async Task<IEnumerable<TReturn>> ExecuteQueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn, object? param = null)
    {
        var connection = _dapperContext.GetConection();
        IEnumerable<TReturn> queryData = await connection.QueryAsync<TFirst, TSecond, TThird, TReturn>(sql, map, param, splitOn: splitOn);

        return queryData;
    }

    public async Task<IEnumerable<T>> ExecuteQueryAsync(string sql, object? param = null)
    {
        var connection = _dapperContext.GetConection();
        IEnumerable<T> queryData = await connection.QueryAsync<T>(sql, param);

        return queryData;
    }

    public string BuildQueryFilters(
        string query,
        List<string>? textVariables = null, 
        List<string>? numberVariables = null,
        List<string>? dateVariables = null)
    {
        if (textVariables == null && numberVariables == null && dateVariables == null)
        {
            return query;
        }

        var firstLoop = true;

        if (textVariables != null)
        {
            foreach (var textVariable in textVariables!)
            {
                query += firstLoop
                    ? $" lower({textVariable}) like @filter"
                    : $" or lower({textVariable}) like @filter";

                firstLoop = false;
            }
        }

        if (numberVariables != null)
        {
            foreach (var numberVariable in numberVariables!)
            {
                query += firstLoop
                    ? $" {numberVariable} = @numberFilter"
                    : $" or {numberVariable} = @numberFilter";

                firstLoop = false;
            }
        }

        if (dateVariables != null)
        {
            foreach (var dateVariable in dateVariables!)
            {
                query += firstLoop
                    ? $" datediff(day, {dateVariable}, @dateFilter) = 0"
                    : $" or datediff(day, {dateVariable}, @dateFilter) = 0";

                firstLoop = false;
            }
        }

        return query;
    }
}