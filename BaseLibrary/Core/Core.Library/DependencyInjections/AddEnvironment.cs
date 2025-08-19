using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Library.DependencyInjections
{
    public static class AddEnvironment
    {
        public static ConfigurationManager AddEnvironmentConfiguration(this ConfigurationManager configuration, string appsettings = "appsettings")
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrEmpty(environmentName))
            {
                string baseDirectory = Directory.GetCurrentDirectory();
                string appSettingsPath = Path.Combine(baseDirectory, $"{appsettings}.json");
                string appSettingsEnvironmentPath = Path.Combine(baseDirectory, $"{appsettings}.{environmentName}.json");

                if (File.Exists(appSettingsPath) || File.Exists(appSettingsEnvironmentPath))
                {
                    configuration
                    .AddEnvironmentVariables()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"{appsettings}.json", reloadOnChange: true, optional: true) // Với optional: true --> Nếu file không tồn tại → ứng dụng vẫn chạy bình thường
                    .AddJsonFile($"{appsettings}.{environmentName}.json", reloadOnChange: true, optional: true)
                    .Build();
                }
            }

            return configuration;
        }

        public static ConfigurationManager AddEnvironmentConfiguration(this WebApplicationBuilder builder, string appsettings = "appsettings")
        {
            var configuration = builder.Configuration;
            var environmentName = builder.Environment.EnvironmentName;
            if (!string.IsNullOrEmpty(environmentName))
            {
                string baseDirectory = Directory.GetCurrentDirectory();
                string appSettingsPath = Path.Combine(baseDirectory, $"{appsettings}.json");
                string appSettingsEnvironmentPath = Path.Combine(baseDirectory, $"{appsettings}.{environmentName}.json");

                if (File.Exists(appSettingsPath) || File.Exists(appSettingsEnvironmentPath))
                {
                    configuration
                    .AddEnvironmentVariables()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"{appsettings}.json", reloadOnChange: true, optional: true) // Với optional: true --> Nếu file không tồn tại → ứng dụng vẫn chạy bình thường
                    .AddJsonFile($"{appsettings}.{environmentName}.json", reloadOnChange: true, optional: true)
                    .Build();
                }
            }

            return configuration;
        }
    }
}
