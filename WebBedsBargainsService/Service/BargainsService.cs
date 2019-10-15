using Externals.ConfigSystem;
using Externals.LoggSystem;
using Externals.WebApiSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebBedsBargainsService.Base;
using WebBedsBargainsService.Dto;
using WebBedsBargainsService.Model;

namespace WebBedsBargainsService.Service
{
    public class BargainsService : IBargainsService
    {
        private readonly IWebApi _webApi;
        private readonly IBSAvailabilityTranslator _translator;
        private readonly IBargainsUrlProvider _urlProvider;
        private readonly ILogger _logger;
        private readonly IBSCache _cache;

        private readonly double _maxWaitingMilliseconds = 1000;
        private const double MIN_WAITING_MILLISECONDS = 200;

        public BargainsService(
            IConfig config,
            IWebApi webApi,
            IBSAvailabilityTranslator translator,
            IBargainsUrlProvider urlProvider,
            ILogger logger,
            IBSCache cache
        ) {
            _webApi = webApi;
            _translator = translator;
            _urlProvider = urlProvider;
            _logger = logger;
            _cache = cache;

            var configuredMilliseconds = config.Get("BS:WaitingMilliseconds");
            if (Int32.TryParse(configuredMilliseconds, out int milliseconds) && milliseconds > MIN_WAITING_MILLISECONDS)
                _maxWaitingMilliseconds = milliseconds;

            _maxWaitingMilliseconds *= 0.95;

            //_maxWaitingMilliseconds = 10000000;
        }

        public List<BSHotelAvailability> GetAvailabilities(int destinationId, int nights)
        {
            var result = new List<BSHotelAvailability>();

            var tasks = new List<Task> { Task.Factory.StartNew(() => RunGetAvailabilities(destinationId, nights, out result)) };

            Task.WaitAll(tasks.ToArray(), TimeSpan.FromMilliseconds(_maxWaitingMilliseconds));

            return result;
        }

        public void RunGetAvailabilities(int destinationId, int nights, out List<BSHotelAvailability> result)
        {
            result = new List<BSHotelAvailability>();

            try
            {
                if (nights > 0)
                    result = GetBSHotelAvailabilities(destinationId, nights);
            }
            catch(Exception e)
            {
                Log("BS: Uncontroled error", e);
            }
        }

        private List<BSHotelAvailability> GetBSHotelAvailabilities(int destinationId, int nights)
        {
            if (!TryGetBSHotelAvailabilitiesFromCache(destinationId, nights, out List<BSHotelAvailability> availabilies))
                if (TryGetBSHotelAvailabilitiesFromWebApi(destinationId, nights, out availabilies))
                    BSHotelAvailabilitiesInCache(destinationId, nights, availabilies);

            return availabilies;
        }

        private void BSHotelAvailabilitiesInCache(int destinationId, int nights, List<BSHotelAvailability> availabilies)
        {
            try
            {
                _cache.SetBSHotelAvailabilities(destinationId, nights, availabilies);
            }
            catch (Exception e)
            {
                Log($"BS: Error setting BSHotelAvailabilities in IBSCache (destinationId: { destinationId }, nights: { nights })", e);
            }
        }

        private bool TryGetBSHotelAvailabilitiesFromWebApi(int destinationId, int nights, out List<BSHotelAvailability> availabilies)
        {
            availabilies = new List<BSHotelAvailability>();

            try
            {
                string url = _urlProvider.GetAvailabilitiesUrl(destinationId, nights);

                List<BSAvailabilityDto> response = GetByWebApi(url);

                availabilies = Translate(response, nights);
            }
            catch (Exception e)
            {
                Log($"BS: Error getting BSHotelAvailabilities from IWebApi (destinationId: { destinationId }, nights: { nights })", e);
            }

            return availabilies.Any();
        }

        private List<BSAvailabilityDto> GetByWebApi(string url)
        {
            List<BSAvailabilityDto> response = null;
            double milliseconds = _maxWaitingMilliseconds;

            while (response == null && milliseconds >= MIN_WAITING_MILLISECONDS)
            {
                var watch = new Stopwatch();
                watch.Start();

                try { response = _webApi.Get<List<BSAvailabilityDto>>(url, milliseconds); } catch(Exception e) { }

                watch.Stop();
                milliseconds -= watch.ElapsedMilliseconds;
            }

            return response ?? new List<BSAvailabilityDto>();
        }

        private List<BSHotelAvailability> Translate(List<BSAvailabilityDto> dtoList, int nights)
        {
            List<BSHotelAvailability> result = new List<BSHotelAvailability>();

            if (dtoList == null)
                return result;

            foreach (var dto in dtoList)
                result.Add(_translator.Translate(dto, nights));

            return result.Where(av => av != null).ToList();
        }

        private bool TryGetBSHotelAvailabilitiesFromCache(int destinationId, int nights, out List<BSHotelAvailability> availabilies)
        {
            availabilies = new List<BSHotelAvailability>();

            try
            {
                availabilies = _cache.GetBSHotelAvailabilities(destinationId, nights) ?? new List<BSHotelAvailability>();
            }
            catch(Exception e)
            {
                Log($"BS: Error getting BSHotelAvailabilities from IBSCache (destinationId: { destinationId }, nights: { nights })", e);
            }

            return availabilies.Any();
        }

        private void Log(string message, Exception e)
        {
            try { _logger.Log(message, e); } catch (Exception) { }
        }
    }
}
