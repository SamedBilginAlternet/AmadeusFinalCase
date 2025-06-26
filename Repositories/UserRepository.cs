using AmadeusFlightAp�.Data;
using AmadeusFlightAp�.Models;
using Microsoft.EntityFrameworkCore;

namespace AmadeusFlightAp�.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
