//@QnSBaseCode
//MdStart
using Microsoft.Extensions.Configuration;

namespace QuickNSmart.Logic.Modules.Configuration
{
    public static partial class Settings
    {
        private static IConfigurationRoot _configurationRoot = null;

        public static void SetConfiguration(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public static string Get(string key)
        {
            var result = default(string);

            if (_configurationRoot != null)
            {
                result = _configurationRoot[key]; 
            }
            return result;
        }
        public static string Get(string key, string defaultValue)
        {
            var result = default(string);

            if (_configurationRoot != null)
            {
                result = _configurationRoot.GetValue<string>(key, defaultValue);
            }
            return result;
        }
        public static IConfigurationSection GetSection(string key)
        {
            var result = default(IConfigurationSection);
                
            if (_configurationRoot != null)
            {
                result = _configurationRoot.GetSection(key);
            }
            return result;
        }
    }
}
//MdEnd