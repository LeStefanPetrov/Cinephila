using Cinephila.DataAccess.Repositories;
using Cinephila.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Cinephila.API.StartupExtensions
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
            return services;
        }
    }
}
