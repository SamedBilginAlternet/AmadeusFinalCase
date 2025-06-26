using AmadeusFlightApý.Models;

namespace AmadeusFlightApý.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUserNameAsync(string userName);
    }
}
