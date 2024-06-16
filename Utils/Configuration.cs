using Microsoft.Extensions.Configuration;
using System.IO;

namespace PlaywrightSpecFlowFramework.Utils
{
    public static class Configuration
    {
        private static IConfigurationRoot _configuration;

        static Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("TestData/appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }

        public static string? GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name);
        }
    }
}
