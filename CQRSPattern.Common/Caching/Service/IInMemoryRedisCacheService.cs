namespace CQRSPattern.Common.Caching.Service;

public interface IInMemoryRedisCacheService<T>
{
    T? Get(string key);
    void Remove(string key);
    T Set(string key, T value);
}