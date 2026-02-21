using Microsoft.Extensions.Configuration;
using System;
using System.IO;

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
            string? inDirectory = _configuration?["Directories:InDirectory"];
            return !String.IsNullOrEmpty(inDirectory) && Directory.Exists(inDirectory) ? inDirectory : String.Empty;
        }
        public static string GetOutDirectory()
        {
            string? outDirectory = _configuration?["Directories:OutDirectory"];
            return !String.IsNullOrEmpty(outDirectory) && Directory.Exists(outDirectory) ? outDirectory : String.Empty;
        }
    }
}
