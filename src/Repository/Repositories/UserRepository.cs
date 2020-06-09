using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Auth;
using Repository.Context;
using Repository.DatabaseModels;

namespace Repository.Repositories
{
    public class UserRepository
    {
        private DbSet<User> _dbSet;

        public UserRepository(AppDbContext context)
        {
            _dbSet = context.Users;
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbSet.AddAsync(user);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _dbSet.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }
    }
}