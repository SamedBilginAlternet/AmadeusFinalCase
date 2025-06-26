using AmadeusFlightApý.Dtos;
using AmadeusFlightApý.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmadeusFlightApý.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> GetAllAsync();
        Task<FlightDto?> GetByIdAsync(Guid id);
        Task<FlightDto> CreateAsync(CreateFlightDto dto);
        Task<FlightDto?> UpdateAsync(Guid id, UpdateFlightDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<FlightDto>> SearchAsync(Guid departureAirportId, Guid arrivalAirportId, DateTime departureDate, DateTime? returnDate);
    }
}
