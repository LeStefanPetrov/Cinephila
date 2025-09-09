using System;
using Cinephila.API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Cinephila.API.StartupExtensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("Authentication").Get<AuthenticationSettings>();

            services
                .AddSwagger(appSettings)
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                        $"{appSettings.Authority.Trim('/')}/.well-known/openid-configuration",
                        new OpenIdConnectConfigurationRetriever(),
                        new HttpDocumentRetriever());

                    var discoveryDocument = configurationManager.GetConfigurationAsync().GetAwaiter().GetResult();

                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.MapInboundClaims = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = discoveryDocument.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeys = discoveryDocument.SigningKeys,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1),
                    };
                });

            return services;
        }
    }
}
