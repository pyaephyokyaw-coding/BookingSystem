using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BCT.CommonLib.Services;

public class RedisCacheService(IDistributedCache cache) : IRedisCacheService
{
    public T Get<T>(string key)
    {
        var value = cache.GetString(key);
        if (value != null)
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        return default;
    }

    public T Set<T>(string key, T value)
    {
        var timeOut = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
            SlidingExpiration = TimeSpan.FromMinutes(60)
        };
        cache.SetString(key, JsonSerializer.Serialize(value), timeOut);
        return value;
    }
}
