using Microsoft.Extensions.Caching.Memory;

namespace CQRSPattern.Common.Caching.Service;

public class InMemoryRedisCacheService<T> : IInMemoryRedisCacheService<T>
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryRedisCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T? Get(string key) => _memoryCache.Get<T>(key);

    public T Set(string key, T value) => _memoryCache.Set(key, value);

    public void Remove(string key) => _memoryCache.Remove(key);
}