using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using VakSight.Shared.Entities;
using VakSight.Shared.Models;

namespace VakSight.Shared.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected OkObjectResult Ok<T>(T content) => base.Ok(new SuccessResultModel<T>(content));

        protected CurrentUser CurrentUser
        {
            get
            {
                var sid = User?.FindFirst(x => x.Type == ClaimTypes.PrimarySid)?.Value;
                var tokenPurpose = User?.FindFirst(x => x.Type == ClaimTypes.UserData)?.Value;
                var roles = User?.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
                var ipAddress = HttpContext?.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;

                return new CurrentUser
                {
                    Id = sid == null ? Guid.Empty : new Guid(sid),
                    IPAddress = ipAddress,
                    TokenPurpose = tokenPurpose,
                    Roles = roles
                };
            }
        }
    }
}
