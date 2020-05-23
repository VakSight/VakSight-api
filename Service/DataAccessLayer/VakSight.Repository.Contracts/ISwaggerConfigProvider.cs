using System;

namespace VakSight.Repository.Contracts
{
    public interface ISwaggerConfigProvider
    {
        SwaggerConfig GetSwaggerConfig();
    }
}
