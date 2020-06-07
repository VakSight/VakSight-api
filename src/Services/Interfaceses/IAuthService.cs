using System.Security.Claims;

namespace Services.Interfaceses
{
    public interface IAuthService
    {
        bool UserAndPasswordAreValid(string username, string password);

        Claim[] GetUserClaims(string username);

        string BuildToken(string username, string password);
    }
}
