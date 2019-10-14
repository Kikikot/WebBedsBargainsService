using System;

namespace Externals.CacheSystem
{
    public interface ICache
    {
        T Get<T>(string key);
        void Set<T>(string key, T item, TimeSpan? expiry = null);
    }
}
