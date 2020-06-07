using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using VakSight.Host.Auth;
using VakSight.Repository.Contracts;
using VakSight.Repository.Implementation;
using VakSight.Repository.Implementation.Auth;
using VakSight.Repository.Implementation.DataAccessObjects;
using VakSight.Repository.Implementation.Database;
using VakSight.Shared;
using VakSight.Shared.Api.Extension;
using VakSight.Shared.Api.Models;

namespace VakSight.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private SwaggerConfig SwaggerConfiguration { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcAndConfigure();

            services.AddIdentity<UserRecord, IdentityRole<Guid>>()
                .AddTokenProvider<JwtTokenProvider>(JwtTokenProvider.Name)
                .AddDefaultTokenProviders();

            services.AddJwtBearerAuthentication(Configuration);
            services.AddSingleton<ISwaggerConfigProvider, SwaggerConfigProvider>();
            services.AddHttpClient();

            services.AddSingleton<IDatabaseConfigReader, DatabaseConfigReader>();
            services.AddSingleton<IDatabaseConfigProvider, DatabaseConfigProvider>();

            AddSwaggerConfig(services);
            services.AddSwagger(SwaggerConfiguration);
            services.AddAutoMapper();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("local"))
            {
                app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
                app.UseDeveloperExceptionPage();
                app.UseSwagger(SwaggerConfiguration);
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void AddSwaggerConfig(IServiceCollection serviceCollection)
        {
            SwaggerConfiguration = serviceCollection.BuildServiceProvider().GetService<ISwaggerConfigProvider>().GetSwaggerConfig();
        }
    }

    public static class AuthSetup
    {
        public static void AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection($"{SettingsConsts.Auth.Name}:{SettingsConsts.Auth.SecretKey}").Value;
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new DefaultTokenValidationParameters(secretKey);
            });

            // Add JwtSecurityTokenValidator after initializing all services
            services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new JwtSecurityTokenValidator(services.BuildServiceProvider()));
            });
        }
    }

    public static class MvcSetup
    {
        public static void AddMvcAndConfigure(this IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = (context) => new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState));
                });
        }
    }
}
