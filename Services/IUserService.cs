using AmadeusFlightAp�.Models;

namespace AmadeusFlightAp�.Services
{
    public interface IUserService
    {
        Task<User?> RegisterAsync(string userName, string password);
        Task<User?> AuthenticateAsync(string userName, string password);
    }
}
