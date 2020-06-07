using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using VakSight.Shared.Api.Models;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using VakSight.Shared.Api.Attributes;

namespace VakSight.Shared.Api.Extension
{
    public static class UseSwaggerConfig
    {
        public static void AddSwagger(this IServiceCollection services, SwaggerConfig swaggerConfig)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = $"{swaggerConfig.Title} API", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.OperationFilter<SwaggerOperationFilter>();

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey",
                });

                c.CustomSchemaIds(x => x.FullName);

                c.AddSecurityRequirement(security);
            });
        }

        public static void UseSwagger(this IApplicationBuilder app, SwaggerConfig swaggerConfig)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                string swaggerEndpoint = string.IsNullOrEmpty(swaggerConfig.ApiHostUrl)
                    ? "/api/swagger/v1/swagger.json"
                    : $"{swaggerConfig.ApiHostUrl}/api/swagger/v1/swagger.json";

                c.SwaggerEndpoint(swaggerEndpoint, $"{swaggerConfig.Title} API V1");
                c.RoutePrefix = "api/swagger";
            });
        }

        private class SwaggerOperationFilter : IOperationFilter
        {
            private const string NoAuthorization = "[ No authorization ]";

            public void Apply(Operation operation, OperationFilterContext context)
            {
                context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo);
                operation.Summary = GetSummary(methodInfo.DeclaringType.CustomAttributes, context.MethodInfo.CustomAttributes);
            }

            private string GetSummary(IEnumerable<CustomAttributeData> controllerAtrributes, IEnumerable<CustomAttributeData> methodAtrributes)
            {
                var allowAnonymousAttribute = methodAtrributes.FirstOrDefault(x => x.AttributeType == typeof(AllowAnonymousAttribute));
                if (allowAnonymousAttribute != null)
                {
                    return NoAuthorization;
                }

                var authorizeAttribute = FindAuthorizeAttribute(methodAtrributes) ?? FindAuthorizeAttribute(controllerAtrributes);

                return NoAuthorization;
            }

            private CustomAttributeData FindAuthorizeAttribute(IEnumerable<CustomAttributeData> attributes)
            {
                return attributes.FirstOrDefault(x => x.AttributeType == typeof(AuthorizeAttribute) || x.AttributeType == typeof(AuthorizeRolesAttribute));
            }

            private string GetRoles(CustomAttributeData attributeData)
            {
                return "All roles";
            }
        }
    }
}
