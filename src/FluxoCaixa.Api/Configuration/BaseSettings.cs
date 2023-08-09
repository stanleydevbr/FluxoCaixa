using Microsoft.Extensions.Configuration;
using System.IO;

namespace FluxoCaixa.Api.Configuration
{
    public static class BaseSettings
    {
        private const string APP_SETTINGS = "appSettings.json";
        public static T Get<T>(this IConfiguration config, string session)
        {
            T result = config.GetSection(session).Get<T>();
            return result;
        }

        public static T Get<T>(string session)
        {
            var config = Config();
            T result = config.GetSection(session).Get<T>();
            return (T)result;
        }

        private static IConfiguration Config()
        {
            var _appSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(APP_SETTINGS)
                .AddJsonFile(APP_SETTINGS.ToLower())
                .Build();
            return _appSettings;
        }
    }
}
