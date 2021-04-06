using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    { 
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<bool> SaltExists(string salt)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Salt == salt);
            if (user != null)
            {
                return false;
            }

            return true;
        }
    }
}