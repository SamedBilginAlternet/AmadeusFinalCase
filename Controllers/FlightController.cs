using AmadeusFlightApý.Dtos;
using AmadeusFlightApý.Models;
using AmadeusFlightApý.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmadeusFlightApý.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<FlightDto>>>> GetFlights()
        {
            var flights = await _flightService.GetAllAsync();
            return Ok(GenericResponse<IEnumerable<FlightDto>>.SuccessResponse(flights));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse<FlightDto>>> GetFlight(Guid id)
        {
            var flight = await _flightService.GetByIdAsync(id);
            if (flight == null)
                return NotFound(GenericResponse<FlightDto>.ErrorResponse($"Flight with id {id} not found."));
            return Ok(GenericResponse<FlightDto>.SuccessResponse(flight));
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<FlightDto>>> CreateFlight([FromBody] CreateFlightDto dto)
        {
            var created = await _flightService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetFlight), new { id = created.Id }, GenericResponse<FlightDto>.SuccessResponse(created, "Flight created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse<FlightDto>>> UpdateFlight(Guid id, [FromBody] UpdateFlightDto dto)
        {
            var updated = await _flightService.UpdateAsync(id, dto);
            if (updated == null)
                return BadRequest(GenericResponse<FlightDto>.ErrorResponse("Id mismatch or flight not found."));
            return Ok(GenericResponse<FlightDto>.SuccessResponse(updated, "Flight updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse<string>>> DeleteFlight(Guid id)
        {
            var deleted = await _flightService.DeleteAsync(id);
            if (!deleted)
                return NotFound(GenericResponse<string>.ErrorResponse($"Flight with id {id} not found."));
            return Ok(GenericResponse<string>.SuccessResponse(null, "Flight deleted successfully."));
        }

        [HttpGet("search")]
        public async Task<ActionResult<GenericResponse<IEnumerable<FlightDto>>>> SearchFlights(
            [FromQuery] Guid departureAirportId,
            [FromQuery] Guid arrivalAirportId,
            [FromQuery] DateTime departureDate,
            [FromQuery] DateTime? returnDate)
        {
            var flights = await _flightService.SearchAsync(departureAirportId, arrivalAirportId, departureDate, returnDate);
            return Ok(GenericResponse<IEnumerable<FlightDto>>.SuccessResponse(flights));
        }
    }
}
