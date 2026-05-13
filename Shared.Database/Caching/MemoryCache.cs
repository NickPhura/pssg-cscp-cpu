using Microsoft.Extensions.Caching.Memory;

namespace Database;

public class MemoryCache : ICache
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration)
    {
        if (!_memoryCache.TryGetValue(key, out T cacheEntry))
        {
            cacheEntry = await factory();

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(expiration);

            _memoryCache.Set(key, cacheEntry, cacheEntryOptions);
        }

        return cacheEntry;
    }
}
