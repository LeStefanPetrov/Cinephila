using Cinephila.API.FluentValidators;
using Cinephila.Domain.DTOs.ParticipantsDTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.StartupExtensions
{
    public static class ValidatorsExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Startup>());
            return services;
        }
    }
}
