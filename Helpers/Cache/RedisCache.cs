using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace InsideAirbnb.Helpers.Cache
{
    public class RedisCache : ICache
    {
        private static IDistributedCache _cache;
        
        public RedisCache(IDistributedCache cache)
        {
            _cache = cache;
        }
        
        public async Task<T> Get<T, K>(K key, string identifier)
        {
            var stringKey = JsonConvert.SerializeObject(key);
            var item = await _cache.GetStringAsync(identifier + stringKey);
            if (item == null) return default(T);
            return JsonConvert.DeserializeObject<T>(item);
        }
        
        public async void Set<T, K>(K key, T value, string identifier)
        {
            var stringKey = JsonConvert.SerializeObject(key);
            await _cache.SetStringAsync(identifier + stringKey, JsonConvert.SerializeObject(value, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }), 
                new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)));
        }

        public async void Invalidate(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async void Validate(string key)
        {
            await _cache.RefreshAsync(key);
        }
    }
}