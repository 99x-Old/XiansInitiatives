using Microsoft.Extensions.Configuration;

namespace XiansInitiative.Configuration.Accessor
{
    public class ConfigurationAccessor : IConfigurationAccessor
    {
        private readonly IConfiguration _config;

        public ConfigurationAccessor(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString(string stringName)

        {
            return this._config[string.Concat("ConnectionStrings:", stringName)];
        }

        public string GetSetting(string key)
        {
            return this._config[key];
        }
    }
}