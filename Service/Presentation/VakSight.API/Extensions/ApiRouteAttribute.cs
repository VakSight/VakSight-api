using Microsoft.AspNetCore.Mvc;

namespace VakSight.API.Extensions
{
    public class ApiRouteAttribute : RouteAttribute
    {
        public ApiRouteAttribute(string route) : base($"{ApiConsts.BaseRoute}/{route}")
        {
        }
    }
}
