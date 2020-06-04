using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VakSight.Host.Auth;
using VakSight.Repository.Contracts.UnitOfWork;
using VakSight.Repository.Implementation.Auth;
using VakSight.Repository.Implementation.DataAccessObjects;
using VakSight.Shared;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<UserRecord, IdentityRole<Guid>>()
                .AddTokenProvider<JwtTokenProvider>(JwtTokenProvider.Name)
                .AddDefaultTokenProviders();

            services.AddJwtBearerAuthentication(Configuration);

            services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
}
