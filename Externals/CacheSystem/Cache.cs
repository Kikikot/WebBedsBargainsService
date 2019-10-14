using System;
using System.Configuration;
using System.Runtime.Caching;

namespace Externals.CacheSystem
{
    public class Cache : ICache
    {
        private static MemoryCache _cache = MemoryCache.Default;

        private TimeSpan? _defaultExpiry;
        private TimeSpan defaultExpiry {
            get
            {
                if (!_defaultExpiry.HasValue)
                {
                    string cacheDefaultMinutes = ConfigurationManager.AppSettings["Cache:DefaultMinutes"];

                    if (!Int32.TryParse(cacheDefaultMinutes, out int defaultMinutes) || defaultMinutes < 1)
                        defaultMinutes = 60;

                    _defaultExpiry = new TimeSpan(0, defaultMinutes, 0);
                }
                return _defaultExpiry.Value;
            }
        }

        public T Get<T>(string key)
        {
            return (T)_cache.Get(key);
        }

        public void Set<T>(string key, T item, TimeSpan? expiry = null)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddSeconds((expiry.HasValue ? expiry.Value : defaultExpiry).TotalSeconds);
            _cache.Add(key, item, cacheItemPolicy);
        }
    }
}
