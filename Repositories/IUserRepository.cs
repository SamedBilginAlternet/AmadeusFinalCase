using AmadeusFlightAp�.Models;

namespace AmadeusFlightAp�.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUserNameAsync(string userName);
    }
}
