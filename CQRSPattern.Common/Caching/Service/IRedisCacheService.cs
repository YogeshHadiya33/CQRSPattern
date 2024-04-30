namespace CQRSPattern.Common.Caching.Service;

public interface IRedisCacheService<T>
{
    T? Get(string key);
    void Remove(string key);
    T Set(string key, T value);
}