using AutoMapper;
using System;
using System.Threading.Tasks;
using VakSight.Entities.Accounts;
using VakSight.Entities.Auth;
using VakSight.Repository.Contracts;
using VakSight.Repository.Implementation.Auth;
using VakSight.Repository.Implementation.DataAccessObjects;

namespace VakSight.Repository.Implementation.Repositories
{
    public class AccountsRepository : IAccountRepository
    {
        private readonly IMapper mapper;

        public async Task<Guid> CreateAccountAsync(CreateAccount user)
        {
            var record = mapper.Map<UserRecord>(user);

            var result = await SignInManager.UserManager.CreateAsync(record);
            return record.Id;
        }

        public Task<LoginResult> LoginAsync(UserLoginData data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogoutAsync(Guid userId, string uniqPurposePart)
        {
            throw new NotImplementedException();
        }
    }
}
