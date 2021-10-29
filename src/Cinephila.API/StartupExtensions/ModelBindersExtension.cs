using Cinephila.API.DataBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Cinephila.API.StartupExtensions
{
    public static class ModelBindersExtension
    {
        public static IServiceCollection AddCustomModelBinders(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new EntityBinderProvider());
            });
            return services;
        }
    }
}
