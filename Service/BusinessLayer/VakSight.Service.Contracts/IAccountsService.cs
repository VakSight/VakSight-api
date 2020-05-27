using System.Threading.Tasks;
using VakSight.Entities.Accounts;
using VakSight.Entities.Auth;
using VakSight.Shared.Entities;

namespace VakSight.Service.Contracts
{
    public interface IAccountsService
    {
        Task<LoginResult> LoginAsync(AccountLoginData data);

        Task<bool> LogoutAsync(CurrentUser currentUser);

        Task<BaseAccount> CreateAccountAsync(CurrentUser currentUser, CreateAccount account);
    }
}
