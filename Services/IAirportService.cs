using AmadeusFlightApý.Models;

namespace AmadeusFlightApý.Services
{
    public interface IAirportService
    {
        Task<IEnumerable<Airport>> GetAllAsync();
        Task<Airport?> GetByIdAsync(Guid id);
        Task<Airport> CreateAsync(Airport airport);
        Task<Airport?> UpdateAsync(Guid id, Airport airport);
        Task<bool> DeleteAsync(Guid id);
    }
}
