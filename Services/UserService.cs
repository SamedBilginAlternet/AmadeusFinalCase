using AmadeusFlightApý.Models;
using AmadeusFlightApý.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace AmadeusFlightApý.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> RegisterAsync(string userName, string password)
        {
            if (await _userRepository.GetByUserNameAsync(userName) != null)
                return null;
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                PasswordHash = HashPassword(password)
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return user;
        }

        public async Task<User?> AuthenticateAsync(string userName, string password)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                return null;
            return user;
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 2) return false;
            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hash == parts[1];
        }
    }
}
