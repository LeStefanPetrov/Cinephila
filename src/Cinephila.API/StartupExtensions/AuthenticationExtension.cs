using Cinephila.API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Cinephila.API.StartupExtensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var _appSettings = configuration.GetSection("Authentication").Get<AuthenticationSettings>();

            services
                .AddSwagger(_appSettings)
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                        $"{_appSettings.Authority.Trim('/')}/.well-known/openid-configuration",
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
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

            return services;
        }
    }
}
