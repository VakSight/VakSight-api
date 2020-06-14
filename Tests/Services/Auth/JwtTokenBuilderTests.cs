using System;
using Xunit;
using Services.Auth;
using Models.Auth;
using System.Security.Claims;

namespace Tests.Services.Auth
{
    public class JwtTokenBuilderTests
    {
        [Fact]
        public void BuildTokenSuccessful()
        {
            var jwtConfig = new JwtConfig
            {
                Audience = "audience",
                Issuer = "issuer",
                SecretKey = "your-256-bit-my-ultra-secret-key-your-256-bit-my-ultra-secret-key"
            };

            var claims = new Claim[] {
                new Claim("name", "John Doe")
            };

            JwtTokenBuilder jwtTokenBuidler = new JwtTokenBuilder(jwtConfig);
            var issuedAt = DateTime.Parse("2019-01-01", System.Globalization.CultureInfo.InvariantCulture);

            var expected = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiSm9obiBEb2UiLCJuYmYiOjE1NDYyOTAwMDAsImV4cCI6MTU0NjI5MDEyMCwiaXNzIjoiaXNzdWVyIiwiYXVkIjoiYXVkaWVuY2UifQ.td6s1n7nvtl4cJDEKFZXObB4w8Jz4e7DHxivOK0QcnY";

            string jwt = jwtTokenBuidler.BuildToken(issuedAt, expirationInMinutes: 2, claims: claims);

            Assert.Equal(expected, jwt);
        }
    }
}