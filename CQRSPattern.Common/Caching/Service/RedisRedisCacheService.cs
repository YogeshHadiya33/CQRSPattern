using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CQRSPattern.Common.Caching.Service;

public class RedisRedisCacheService<T> : IRedisCacheService<T>
{
    private readonly IDistributedCache _distributedCache;

    public RedisRedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public T? Get(string key)
    {
        var responseString = _distributedCache.GetString(key);

        return !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<T>(responseString) : default(T);
    }

    public T Set(string key, T value)
    {
        var stringValue = JsonConvert.SerializeObject(value);
        _distributedCache.SetString(key, stringValue);
        return value;
    }

    public void Remove(string key) => _distributedCache.Remove(key);
}