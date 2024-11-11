using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.Services;
using Cinephila.Services.BackgroundServices;
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

        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonFetcherService, PersonFetcherService>();
            services.AddSingleton<IMovieFetcherService, MovieFetcherService>();

            services.AddHostedService<DataFetcherService>();

            return services;
        }
    }
}
