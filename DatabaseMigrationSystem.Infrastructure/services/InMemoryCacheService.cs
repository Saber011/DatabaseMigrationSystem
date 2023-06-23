using Microsoft.Extensions.Caching.Memory;

namespace DatabaseMigrationSystem.Infrastructure.services;

public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public InMemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        return _cache.TryGetValue(key, out T value) ? value : default(T);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions 
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(5)
        };

        _cache.Set(key, value, cacheEntryOptions);
    }

    public async Task RemoveAsync(string key)
    {
        _cache.Remove(key);
    }
}
