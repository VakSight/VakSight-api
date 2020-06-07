using Microsoft.AspNetCore.Mvc;
using VakSight.Shared.Api;

namespace VakSight.API.Extensions
{
    public class ApiRouteAttribute : RouteAttribute
    {
        public ApiRouteAttribute(string route) : base($"{ApiConsts.BaseRoute}/{route}")
        {
        }
    }
}
