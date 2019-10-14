using Externals.CacheSystem;
using Externals.ConfigSystem;
using Externals.LoggSystem;
using System;
using System.Collections.Generic;
using WebBedsBargainsService.Base;
using WebBedsBargainsService.Model;

namespace WebBedsBargainsService.Caches
{
    public class BSCache : IBSCache
    {
        private readonly IConfig _config;
        private readonly ICache _cache;
        private readonly ILogger _logger;

        private readonly TimeSpan? _expiry;

        public BSCache(
            IConfig config,
            ICache cache,
            ILogger logger
        ) {
            _config = config;
            _cache = cache;
            _logger = logger;

            string cacheMinutes = _config.Get("BS:CacheMinutes");
            if (Int32.TryParse(cacheMinutes, out int minutes) && minutes > 0)
                _expiry = new TimeSpan(0, minutes, 0);
        }

        private T Get<T>(string key)
        {
            T result = default;

            if (_expiry.HasValue)
                try
                {
                    result = _cache.Get<T>(key);
                }
                catch (Exception e)
                {
                    Log($"BS: Error getting key '{ key }' from ICache", e);
                }

            return result;
        }

        private void Log(string message, Exception e)
        {
            try { _logger.Log(message, e); } catch (Exception) { }
        }

        private void Set<T>(string key, T item)
        {
            if (_expiry.HasValue)
                try
                {
                    _cache.Set(key, item, _expiry);
                }
                catch(Exception e)
                {
                    Log($"BS: Error setting key '{ key }' in ICache", e);
                }
        }

        public List<BSHotelAvailability> GetBSHotelAvailabilities(int destinationId, int nights)
        {
            return Get<List<BSHotelAvailability>>(GetBSHotelAvailabilitiesKey(destinationId, nights));
        }

        public void SetBSHotelAvailabilities(int destinationId, int nights, List<BSHotelAvailability> result)
        {
            Set(GetBSHotelAvailabilitiesKey(destinationId, nights), result);
        }

        private string GetBSHotelAvailabilitiesKey(int destinationId, int nights)
        {
            return $"[BS:BSHotelAvailabilities:DestinationId:{ destinationId }:Nights:{ nights }]";
        }
    }
}
