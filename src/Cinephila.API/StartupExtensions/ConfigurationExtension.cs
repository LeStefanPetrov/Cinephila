using Cinephila.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cinephila.API.StartupExtensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection LoadConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection("MovieApi"));

            return services;
        }
    }
}
