using Services.Interfaceses;
using System;
using System.Security.Claims;

namespace Services.Auth
{
    public class AuthService : IAuthService
    {
        private IJwtTokenBuilder _jwtTokenBuilder;

        public AuthService(IJwtTokenBuilder jwtTokenBuilder)
        {
            _jwtTokenBuilder = jwtTokenBuilder;
        }

        public bool UserAndPasswordAreValid(string username, string password)
        {
            return true;
        }

        public Claim[] GetUserClaims(String username)
        {
            return new []
            {
                new Claim("Id", "1"),
                new Claim(ClaimTypes.Name, username)
            };
        }

        public string BuildToken(string username, string password)
        {
            if (!UserAndPasswordAreValid(username, password))
            {
                return null;
            }
            
            return _jwtTokenBuilder.BuildToken(DateTime.Now, 30, GetUserClaims(username));
        }
    }
}