using Cinephila.Domain.Services;
using Cinephila.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cinephila.API.StartupExtensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IParticipantsService, ParticipantsService>();
            services.AddScoped<IProductionsService, ProductionsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IReviewsService, ReviewsService>();

            return services;
        }
    }
}
