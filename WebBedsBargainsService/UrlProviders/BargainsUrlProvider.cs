using Externals.ConfigSystem;
using System;
using WebBedsBargainsService.Base;

namespace WebBedsBargainsService.UrlProviders
{
    public class BargainsUrlProvider : IBargainsUrlProvider
    {
        private readonly IConfig _config;

        private readonly string _endpoint;
        private readonly string _userCode;

        private const string _find = "/api/findBargain?destinationId=###_DESTINATION_ID_###&nights=###_NIGHTS_###&code=###_USER_CODE_###";

        public BargainsUrlProvider(IConfig config)
        {
            _config = config;

            _endpoint = _config.Get("BS:BargainsEndPoint");
            _userCode = _config.Get("BS:BargainsUserCode");
        }

        public string GetAvailabilitiesUrl(int destination, int nights)
        {
            CheckConfig();

            return string.Format(
                "{0}{1}",
                _endpoint,
                _find
                    .Replace("###_DESTINATION_ID_###", destination.ToString())
                    .Replace("###_NIGHTS_###", nights.ToString())
                    .Replace("###_USER_CODE_###", _userCode)
            );
        }

        private void CheckConfig()
        {
            if (string.IsNullOrEmpty(_endpoint))
                ThrowError("EndPoint");
            
            if (string.IsNullOrEmpty(_userCode))
                ThrowError("UserCode");
        }

        private void ThrowError(string key)
        {
            throw new Exception($"Config key 'Bargains:{ key }' not provided.");
        }
    }
}
