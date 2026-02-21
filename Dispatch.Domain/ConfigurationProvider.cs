using Microsoft.Extensions.Configuration;

namespace Dispatch.Domain
{
    public static class ConfigurationProvider
    {
        private static IConfiguration? _configuration;

        public static void Initialize(string configPath)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(configPath) ?? AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static string GetInDirectory()
        {
            return _configuration?["Directories:InDirectory"] ?? throw new InvalidOperationException("InDirectory not configured");
        }

        public static string GetOutDirectory()
        {
            return _configuration?["Directories:OutDirectory"] ?? throw new InvalidOperationException("OutDirectory not configured");
        }
    }
}
