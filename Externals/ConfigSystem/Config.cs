using System.Configuration;

namespace Externals.ConfigSystem
{
    public class Config : IConfig
    {
        public string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
