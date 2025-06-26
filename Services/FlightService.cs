using AmadeusFlightApý.Dtos;
using AmadeusFlightApý.Models;
using AmadeusFlightApý.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AmadeusFlightApý.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IAirportRepository _airportRepository;
        public FlightService(IFlightRepository flightRepository, IAirportRepository airportRepository)
        {
            _flightRepository = flightRepository;
            _airportRepository = airportRepository;
        }

        public async Task<IEnumerable<FlightDto>> GetAllAsync()
        {
            var flights = await _flightRepository.GetAllAsync();
            var airports = (await _airportRepository.GetAllAsync()).ToDictionary(a => a.Id, a => a.City);
            return flights.Select(f => new FlightDto
            {
                Id = f.Id,
                DepartureAirportId = f.DepartureAirportId,
                ArrivalAirportId = f.ArrivalAirportId,
                DepartureDateTime = f.DepartureDateTime,
                ReturnDateTime = f.ReturnDateTime,
                Price = f.Price,
                DepartureAirportCity = airports.ContainsKey(f.DepartureAirportId) ? airports[f.DepartureAirportId] : null,
                ArrivalAirportCity = airports.ContainsKey(f.ArrivalAirportId) ? airports[f.ArrivalAirportId] : null
            });
        }

        public async Task<FlightDto?> GetByIdAsync(Guid id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null) return null;
            var depAirport = await _airportRepository.GetByIdAsync(flight.DepartureAirportId);
            var arrAirport = await _airportRepository.GetByIdAsync(flight.ArrivalAirportId);
            return new FlightDto
            {
                Id = flight.Id,
                DepartureAirportId = flight.DepartureAirportId,
                ArrivalAirportId = flight.ArrivalAirportId,
                DepartureDateTime = flight.DepartureDateTime,
                ReturnDateTime = flight.ReturnDateTime,
                Price = flight.Price,
                DepartureAirportCity = depAirport?.City,
                ArrivalAirportCity = arrAirport?.City
            };
        }

        public async Task<FlightDto> CreateAsync(CreateFlightDto dto)
        {
            var flight = new Flight
            {
                Id = Guid.NewGuid(),
                DepartureAirportId = dto.DepartureAirportId,
                ArrivalAirportId = dto.ArrivalAirportId,
                DepartureDateTime = dto.DepartureDateTime,
                ReturnDateTime = dto.ReturnDateTime,
                Price = dto.Price
            };
            await _flightRepository.AddAsync(flight);
            await _flightRepository.SaveChangesAsync();
            var depAirport = await _airportRepository.GetByIdAsync(flight.DepartureAirportId);
            var arrAirport = await _airportRepository.GetByIdAsync(flight.ArrivalAirportId);
            return new FlightDto
            {
                Id = flight.Id,
                DepartureAirportId = flight.DepartureAirportId,
                ArrivalAirportId = flight.ArrivalAirportId,
                DepartureDateTime = flight.DepartureDateTime,
                ReturnDateTime = flight.ReturnDateTime,
                Price = flight.Price,
                DepartureAirportCity = depAirport?.City,
                ArrivalAirportCity = arrAirport?.City
            };
        }

        public async Task<FlightDto?> UpdateAsync(Guid id, UpdateFlightDto dto)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null) return null;
            flight.DepartureDateTime = dto.DepartureDateTime;
            flight.ReturnDateTime = dto.ReturnDateTime;
            flight.Price = dto.Price;
            _flightRepository.Update(flight);
            await _flightRepository.SaveChangesAsync();
            var depAirport = await _airportRepository.GetByIdAsync(flight.DepartureAirportId);
            var arrAirport = await _airportRepository.GetByIdAsync(flight.ArrivalAirportId);
            return new FlightDto
            {
                Id = flight.Id,
                DepartureAirportId = flight.DepartureAirportId,
                ArrivalAirportId = flight.ArrivalAirportId,
                DepartureDateTime = flight.DepartureDateTime,
                ReturnDateTime = flight.ReturnDateTime,
                Price = flight.Price,
                DepartureAirportCity = depAirport?.City,
                ArrivalAirportCity = arrAirport?.City
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null) return false;
            _flightRepository.Delete(flight);
            await _flightRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FlightDto>> SearchAsync(Guid departureAirportId, Guid arrivalAirportId, DateTime departureDate, DateTime? returnDate)
        {
            var flights = await _flightRepository.FindAsync(f =>
                f.DepartureAirportId == departureAirportId &&
                f.ArrivalAirportId == arrivalAirportId &&
                f.DepartureDateTime.Date == departureDate.Date
            );
            var airports = (await _airportRepository.GetAllAsync()).ToDictionary(a => a.Id, a => a.City);
            var result = flights.Select(f => new FlightDto
            {
                Id = f.Id,
                DepartureAirportId = f.DepartureAirportId,
                ArrivalAirportId = f.ArrivalAirportId,
                DepartureDateTime = f.DepartureDateTime,
                ReturnDateTime = f.ReturnDateTime,
                Price = f.Price,
                DepartureAirportCity = airports.ContainsKey(f.DepartureAirportId) ? airports[f.DepartureAirportId] : null,
                ArrivalAirportCity = airports.ContainsKey(f.ArrivalAirportId) ? airports[f.ArrivalAirportId] : null
            });
            if (returnDate.HasValue)
            {
                var returnFlights = await _flightRepository.FindAsync(f =>
                    f.DepartureAirportId == arrivalAirportId &&
                    f.ArrivalAirportId == departureAirportId &&
                    f.DepartureDateTime.Date == returnDate.Value.Date
                );
                result = result.Concat(returnFlights.Select(f => new FlightDto
                {
                    Id = f.Id,
                    DepartureAirportId = f.DepartureAirportId,
                    ArrivalAirportId = f.ArrivalAirportId,
                    DepartureDateTime = f.DepartureDateTime,
                    ReturnDateTime = f.ReturnDateTime,
                    Price = f.Price,
                    DepartureAirportCity = airports.ContainsKey(f.DepartureAirportId) ? airports[f.DepartureAirportId] : null,
                    ArrivalAirportCity = airports.ContainsKey(f.ArrivalAirportId) ? airports[f.ArrivalAirportId] : null
                }));
            }
            return result;
        }
    }
}
