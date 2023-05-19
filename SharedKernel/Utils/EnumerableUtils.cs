namespace SharedKernel.Utils;

public static class EnumerableUtils<T> where T : class
{
    public static IEnumerable<T> Sort(IEnumerable<T> enumerable, Func<T, string> linq, bool orderAsc)
        => orderAsc ? enumerable.OrderBy(linq) : enumerable.OrderByDescending(linq);
    public static IEnumerable<T> Sort(IEnumerable<T> enumerable, Func<T, int> linq, bool orderAsc)
        => orderAsc ? enumerable.OrderBy(linq) : enumerable.OrderByDescending(linq);
    public static IEnumerable<T> Sort(IEnumerable<T> enumerable, Func<T, decimal> linq, bool orderAsc)
        => orderAsc ? enumerable.OrderBy(linq) : enumerable.OrderByDescending(linq);
    public static IEnumerable<T> Sort(IEnumerable<T> enumerable, Func<T, Guid> linq, bool orderAsc)
        => orderAsc ? enumerable.OrderBy(linq) : enumerable.OrderByDescending(linq);
    public static IEnumerable<T> Sort(IEnumerable<T> enumerable, Func<T, bool> linq, bool orderAsc)
        => orderAsc ? enumerable.OrderBy(linq) : enumerable.OrderByDescending(linq);
    public static IEnumerable<T> Sort(IEnumerable<T> enumerable, Func<T, DateTime> linq, bool orderAsc)
        => orderAsc ? enumerable.OrderBy(linq) : enumerable.OrderByDescending(linq);
}