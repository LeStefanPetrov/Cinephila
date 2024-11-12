using Cinephila.DataAccess.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace Cinephila.API.StartupExtensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DtoToEntityMappingProfile));
            services.AddAutoMapper(typeof(ModelToDtoMappingProfile));

            return services;
        }
    }
}
