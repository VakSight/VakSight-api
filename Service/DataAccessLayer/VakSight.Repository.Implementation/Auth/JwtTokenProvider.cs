using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VakSight.Entities.Auth;
using VakSight.Repository.Implementation.DataAccessObjects;
using VakSight.Shared;

namespace VakSight.Repository.Implementation.Auth
{
    public class JwtTokenProvider : IUserTwoFactorTokenProvider<UserRecord>
    {
        public const string Name = "Jwt";

        private readonly string secretKey;
        private readonly int accessTokenExpiryMinutes;
        private readonly int refreshTokenExpiryMinutes;

        public JwtTokenProvider(IConfiguration configuration)
        {
            var authSection = configuration.GetSection(SettingsConsts.Auth.Name);
            secretKey = authSection.GetValue<string>(SettingsConsts.Auth.SecretKey);
            accessTokenExpiryMinutes = authSection.GetValue<int>(SettingsConsts.Auth.AccessTokenExpiryMinutes);
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<UserRecord> manager, UserRecord user)
        {
            return Task.FromResult(false);
        }

        public async Task<string> GenerateAsync(string purpose, UserManager<UserRecord> manager, UserRecord user)
        {
            var roles = await manager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, string.Join(",", roles)),
                new Claim(ClaimTypes.UserData, purpose)
             };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(accessTokenExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<AccessToken> GenerateAccessTokenAsync(string purpose, UserManager<UserRecord> manager, UserRecord user)
        {
            var token = await GenerateAsync(purpose, manager, user);
            return new AccessToken
            {
                Token = token,
                ExpiresIn = accessTokenExpiryMinutes * 60
            };
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<UserRecord> manager, UserRecord user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new DefaultTokenValidationParameters(secretKey), out SecurityToken securityToken);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}
