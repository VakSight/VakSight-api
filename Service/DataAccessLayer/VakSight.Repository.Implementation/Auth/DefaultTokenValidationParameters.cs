using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace VakSight.Repository.Implementation.Auth
{
    public class DefaultTokenValidationParameters : TokenValidationParameters
    {
        public DefaultTokenValidationParameters(string secretKey)
        {
            ClockSkew = new TimeSpan(0, 1, 0);
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            RequireExpirationTime = true;
            ValidateIssuer = false;
            ValidateIssuerSigningKey = true;
            ValidateAudience = false;
        }
    }
}
