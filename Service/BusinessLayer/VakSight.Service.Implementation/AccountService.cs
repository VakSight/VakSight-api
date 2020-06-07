using System.Threading.Tasks;
using VakSight.Entities.Accounts;
using VakSight.Entities.Auth;
using VakSight.Service.Contracts;
using VakSight.Shared.Entities;

namespace VakSight.Service.Implementation
{
    public class AccountService : IAccountsService
    {
        public Task<BaseAccount> CreateAccountAsync(CurrentUser currentUser, CreateAccount account)
        {
            throw new System.NotImplementedException();
        }

        public Task<LoginResult> LoginAsync(AccountLoginData data)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> LogoutAsync(CurrentUser currentUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> VerifyUserTokenAsync(string userId, string uniqPurposePart, string accessToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
