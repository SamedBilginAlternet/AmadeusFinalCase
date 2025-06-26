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

        public async Task<IEnumerable<Airport>> GetAllAsync() => await _airportRepository.GetAllAsync();

        public async Task<Airport?> GetByIdAsync(Guid id) => await _airportRepository.GetByIdAsync(id);

        public async Task<Airport> CreateAsync(Airport airport)
        {
            airport.Id = Guid.NewGuid();
            await _airportRepository.AddAsync(airport);
            await _airportRepository.SaveChangesAsync();
            return airport;
        }

        public async Task<Airport?> UpdateAsync(Guid id, Airport airport)
        {
            if (id != airport.Id) return null;
            _airportRepository.Update(airport);
            await _airportRepository.SaveChangesAsync();
            return airport;
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
