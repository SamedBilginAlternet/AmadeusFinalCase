using AmadeusFlightApý.Models;
using AmadeusFlightApý.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AmadeusFlightApý.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            // Departure ve Arrival Airport navigation property'lerini include et
            return await _flightRepository.FindAsync(f => true);
        }

        public async Task<Flight?> GetByIdAsync(Guid id)
        {
            return await _flightRepository.GetByIdAsync(id);
        }

        public async Task<Flight> CreateAsync(Flight flight)
        {
            flight.Id = Guid.NewGuid();
            await _flightRepository.AddAsync(flight);
            await _flightRepository.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight?> UpdateAsync(Guid id, Flight flight)
        {
            if (id != flight.Id) return null;
            _flightRepository.Update(flight);
            await _flightRepository.SaveChangesAsync();
            return flight;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null) return false;
            _flightRepository.Delete(flight);
            await _flightRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Flight>> SearchAsync(Guid departureAirportId, Guid arrivalAirportId, DateTime departureDate, DateTime? returnDate)
        {
            var flights = await _flightRepository.FindAsync(f =>
                f.DepartureAirportId == departureAirportId &&
                f.ArrivalAirportId == arrivalAirportId &&
                f.DepartureDateTime.Date == departureDate.Date
            );

            if (returnDate.HasValue)
            {
                var returnFlights = await _flightRepository.FindAsync(f =>
                    f.DepartureAirportId == arrivalAirportId &&
                    f.ArrivalAirportId == departureAirportId &&
                    f.DepartureDateTime.Date == returnDate.Value.Date
                );
                return flights.Concat(returnFlights);
            }
            return flights;
        }
    }
}
