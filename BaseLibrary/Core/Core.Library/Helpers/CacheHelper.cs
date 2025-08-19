using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Core.Library.Helpers
{
    public class CacheHelper
    {
        private readonly IDistributedCache _cache;

        public CacheHelper(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> Get<T>(string key) where T : class
        {
            var cachedResponse = await _cache.GetStringAsync(key);
            return cachedResponse == null ? null : JsonSerializer.Deserialize<T>(cachedResponse);
        }

        public async Task Set<T>(string key, T value, DistributedCacheEntryOptions options) where T : class
        {
            var response = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, response, options);
        }

        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
