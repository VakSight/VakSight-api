using System;
using VakSight.Shared.Api.Models;

namespace VakSight.Repository.Contracts
{
    public interface ISwaggerConfigProvider
    {
        SwaggerConfig GetSwaggerConfig();
    }
}
