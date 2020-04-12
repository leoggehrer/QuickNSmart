//@QnSBaseCode
//MdStart
using System;
using Microsoft.Extensions.Configuration;

namespace CommonBase.Modules.Configuration
{
    public static class Configurator
    {
        public static IConfigurationRoot LoadAppSettings()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
//MdEnd