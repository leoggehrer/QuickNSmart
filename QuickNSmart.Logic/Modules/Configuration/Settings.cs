//@QnSBaseCode
//MdStart
using Microsoft.Extensions.Configuration;
using CommonBase.Extensions;

namespace QuickNSmart.Logic.Modules.Configuration
{
    public static partial class Settings
    {
        private static IConfiguration _configuration;

        public static void SetConfiguration(IConfiguration configuration)
        {
            configuration.CheckArgument(nameof(configuration));

            _configuration = configuration;
        }

        public static string Get(string key)
        {
            var result = default(string);

            if (_configuration != null)
            {
                result = _configuration[key]; 
            }
            return result;
        }
        public static string Get(string key, string defaultValue)
        {
            var result = defaultValue;

            if (_configuration != null)
            {
                result = _configuration.GetValue<string>(key, defaultValue);
            }
            return result;
        }
        public static IConfigurationSection GetSection(string key)
        {
            var result = default(IConfigurationSection);
                
            if (_configuration != null)
            {
                result = _configuration.GetSection(key);
            }
            return result;
        }
    }
}
//MdEnd