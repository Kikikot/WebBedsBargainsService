using Externals.ConfigSystem;
using System.Collections.Generic;

namespace WebBedsBargariansService.UnitTests._Emulation
{
    public class EmulatedConfig : IConfig
    {
        private static Dictionary<string, string> _config = new Dictionary<string, string>();

        public void Set(string key, string value)
        {
            if (_config.ContainsKey(key))
                _config[key] = value;

            else
                _config.Add(key, value);
        }

        public void Clean()
        {
            _config = new Dictionary<string, string>();
        }

        public string Get(string key)
        {
            if (_config.ContainsKey(key))
                return _config[key];

            return null;
        }
    }
}
