using AmadeusFlightApý.Dtos;
using AmadeusFlightApý.Models;
using AmadeusFlightApý.Repositories;

namespace AmadeusFlightApý.Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;
        public AirportService(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<IEnumerable<AirportDto>> GetAllAsync()
        {
            var airports = await _airportRepository.GetAllAsync();
            return airports.Select(a => new AirportDto { Id = a.Id, City = a.City });
        }

        public async Task<AirportDto?> GetByIdAsync(Guid id)
        {
            var airport = await _airportRepository.GetByIdAsync(id);
            if (airport == null) return null;
            return new AirportDto { Id = airport.Id, City = airport.City };
        }

        public async Task<AirportDto> CreateAsync(CreateAirportDto dto)
        {
            var airport = new Airport { Id = Guid.NewGuid(), City = dto.City };
            await _airportRepository.AddAsync(airport);
            await _airportRepository.SaveChangesAsync();
            return new AirportDto { Id = airport.Id, City = airport.City };
        }

        public async Task<AirportDto?> UpdateAsync(Guid id, UpdateAirportDto dto)
        {
            var airport = await _airportRepository.GetByIdAsync(id);
            if (airport == null) return null;
            airport.City = dto.City;
            _airportRepository.Update(airport);
            await _airportRepository.SaveChangesAsync();
            return new AirportDto { Id = airport.Id, City = airport.City };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var airport = await _airportRepository.GetByIdAsync(id);
            if (airport == null) return false;
            _airportRepository.Delete(airport);
            await _airportRepository.SaveChangesAsync();
            return true;
        }
    }
}
