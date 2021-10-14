using Cinephila.Domain.Services;
using Cinephila.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.StartupExtensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IParticipantsService, ParticipantsService>();
            return services;
        }
    }
}
