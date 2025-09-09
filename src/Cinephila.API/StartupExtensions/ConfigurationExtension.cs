using System.Text.Json;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cinephila.API.StartupExtensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection LoadConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection("MovieApi"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ApiSettings>>().Value);

            return services;
        }

        public static IServiceCollection LoadSerializationOptions(this IServiceCollection services)
        {
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            return services;
        }
    }
}
