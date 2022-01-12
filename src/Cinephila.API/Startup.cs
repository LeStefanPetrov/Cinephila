using Cinephila.API.Settings;
using Cinephila.API.StartupExtensions;
using Cinephila.DataAccess;
using Google.Apis.Gmail.v1;
using Google.Apis.Oauth2.v2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;

namespace Cinephila.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CinephilaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CinephilaDb")));
            services.AddControllers();

            var _appSettings = Configuration.GetSection("Authentication").Get<AuthenticationSettings>();

            services
                .AddSwagger(_appSettings)
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(async x =>
                {
                    var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                        $"{_appSettings.Authority}/",
                        new OpenIdConnectConfigurationRetriever(),
                        new HttpDocumentRetriever());

                    var discoveryDocument = await configurationManager.GetConfigurationAsync();

                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = discoveryDocument.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeys = discoveryDocument.SigningKeys,
                        ValidAudience = _appSettings.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

            services.AddMappingProfiles();
            services.AddRepositories();
            services.AddServices();
            services.AddValidators();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CinephilaDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinephila.API v1");
                    c.DocumentTitle = $"Cinephila Service";
                    c.OAuthClientId("21758989588-o99527rg1tidhva82aigfg1u6ku81b6q.apps.googleusercontent.com");
                    c.OAuthClientSecret("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");
                    c.OAuthScopes(new string[] { GmailService.Scope.GmailReadonly , Oauth2Service.Scope.UserinfoProfile, Oauth2Service.Scope.UserinfoEmail });
                    c.EnableDeepLinking();
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            CinephilaDbDataSeeder.SeedCountries(context);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
