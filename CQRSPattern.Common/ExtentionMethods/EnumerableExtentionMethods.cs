namespace CQRSPattern.Common.ExtentionMethods;

public static class EnumerableExtentionMethods
{
    public static bool HasValue<T>(this IEnumerable<T> enumerable)
    {
        return enumerable is not null && enumerable.Any();
    }
}