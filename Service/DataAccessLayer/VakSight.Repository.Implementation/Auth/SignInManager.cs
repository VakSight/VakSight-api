using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using VakSight.Repository.Implementation.DataAccessObjects;

namespace VakSight.Repository.Implementation.Auth
{
    public class SignInManager : SignInManager<UserRecord>
    {
        public SignInManager(UserManager userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<UserRecord> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<UserRecord>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {

        }

        public static SignInManager Create(UserManager userManager, IServiceProvider serviceProvider)
        {
            var contextAccessor = serviceProvider.Resolve<IHttpContextAccessor>();
            var optionsAccessor = serviceProvider.Resolve<IOptions<IdentityOptions>>();
            var logger = serviceProvider.Resolve<ILogger<SignInManager>>();
            var schemes = serviceProvider.Resolve<IAuthenticationSchemeProvider>();
            var claimsFactory = new UserClaimsPrincipalFactory<UserRecord>(userManager, optionsAccessor);

            return new SignInManager(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes);
        }
    }
}
