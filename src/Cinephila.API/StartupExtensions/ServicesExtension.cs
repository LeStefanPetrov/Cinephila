using Cinephila.API.Settings;
using Cinephila.Domain.BackgroundServices;
using Cinephila.Domain.Services;
using Cinephila.Domain.Settings;
using Cinephila.Services.BackgroundServices;
using Cinephila.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            services.AddHostedService<DataFetcherService>();

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var _appSettings = configuration.GetSection("MovieApi").Get<ApiSettings>();


            services.AddHttpClient<IPersonFetcherService, PersonFetcherService>(client =>
            {
                client.BaseAddress = new Uri(_appSettings.Url);
            });

            services.AddHttpClient<IMovieFetcherService, MovieFetcherService>(client =>
            {
                client.BaseAddress = new Uri(_appSettings.Url);
            });

            services.AddHttpClient<IGenreFetcherService, GenreFetcherService>(client =>
            {
                client.BaseAddress = new Uri(_appSettings.Url);
            });

            return services;
        }
    }
}
