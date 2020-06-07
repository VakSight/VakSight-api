using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
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

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            return await _dbSet.Where(u => u.Id == null).Select(u => new UserDTO
            {
                Email = u.Email,
                Password = u.Password
            }).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(UserDTO user)
        {
            var newUser = new User { Email = user.Email, Password = user.Password };
            await _dbSet.AddAsync(newUser);
        }

    }
}