using System;
using System.Threading.Tasks;
using VakSight.Entities.Accounts;
using VakSight.Entities.Auth;

namespace VakSight.Repository.Contracts
{
    public interface IAccountRepository
    {
        Task<Guid> CreateAccountAsync(CreateAccount user);

        Task<LoginResult> LoginAsync(UserLoginData data);

        Task<bool> LogoutAsync(Guid userId, string uniqPurposePart);
    }
}
