using AmadeusFlightApý.Models;

namespace AmadeusFlightApý.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<Flight?> GetByIdAsync(Guid id);
        Task<Flight> CreateAsync(Flight flight);
        Task<Flight?> UpdateAsync(Guid id, Flight flight);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Flight>> SearchAsync(Guid departureAirportId, Guid arrivalAirportId, DateTime departureDate, DateTime? returnDate);
    }
}
