using AmadeusFlightApý.Models;

namespace AmadeusFlightApý.Services
{
    public interface IUserService
    {
        Task<User?> RegisterAsync(string userName, string password);
        Task<User?> AuthenticateAsync(string userName, string password);
    }
}
