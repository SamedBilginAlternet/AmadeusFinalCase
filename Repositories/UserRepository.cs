using AmadeusFlightApý.Data;
using AmadeusFlightApý.Models;
using Microsoft.EntityFrameworkCore;

namespace AmadeusFlightApý.Repositories
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
