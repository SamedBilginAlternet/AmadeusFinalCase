using AmadeusFlightApý.Dtos;
using AmadeusFlightApý.Models;

namespace AmadeusFlightApý.Services
{
    public interface IAirportService
    {
        Task<IEnumerable<AirportDto>> GetAllAsync();
        Task<AirportDto?> GetByIdAsync(Guid id);
        Task<AirportDto> CreateAsync(CreateAirportDto dto);
        Task<AirportDto?> UpdateAsync(Guid id, UpdateAirportDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
