using Cinephila.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Cinephila.API.StartupExtensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CinephilaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CinephilaDb")));
            services.AddSingleton(options =>
            {
                ConfigurationOptions configurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
                ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(configurationOptions);

                return connection;
            });
            return services;
        }
    }
}
