using System;

namespace AmadeusFlightApý.Models
{
    /// <summary>
    /// Represents an application user. Only holds user data. All business logic should be handled in services.
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "User";
        // If needed, extend with navigation properties or additional fields (e.g. Email, IsActive, etc.)
    }
}
