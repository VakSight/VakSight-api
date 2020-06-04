using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VakSight.Service.Contracts;

namespace VakSight.Host.Auth
{
    public class JwtSecurityTokenValidator : JwtSecurityTokenHandler
    {
        private readonly ServiceProvider serviceProvider;

        public JwtSecurityTokenValidator(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public override ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            var result = base.ValidateToken(securityToken, validationParameters, out validatedToken);

            var userId = result.FindFirstValue(ClaimTypes.PrimarySid);
            var uniqPurposePart = result.FindFirstValue(ClaimTypes.UserData);

            var accountService = serviceProvider.GetService<IAccountsService>();

            bool isValid = accountService.VerifyUserTokenAsync(userId, uniqPurposePart, securityToken).Result;
            if (!isValid)
            {
                throw new SecurityTokenException();
            }

            return result;
        }
    }
}
