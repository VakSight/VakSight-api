using Microsoft.Extensions.Configuration;
using VakSight.Repository.Contracts;
using VakSight.Shared;
using VakSight.Shared.Api.Models;

namespace VakSight.Repository.Implementation
{
    public class SwaggerConfigProvider : ISwaggerConfigProvider
    {
        private const string SwaggerTitle = "Softbank FinSight";
        private readonly string _apiHostUrl;

        public SwaggerConfigProvider(IConfiguration config)
        {
            _apiHostUrl = config.GetValue<string>(SettingsConsts.ApiHostUrl.Name);
        }

        public SwaggerConfig GetSwaggerConfig()
        {
            return new SwaggerConfig()
            {
                ApiHostUrl = _apiHostUrl,
                Title = SwaggerTitle
            };
        }
    }
}
