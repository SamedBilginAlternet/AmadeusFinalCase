using AmadeusFlightApý.Models;
using AmadeusFlightApý.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmadeusFlightApý.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;
        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Airport>>>> GetAirports()
        {
            var airports = await _airportService.GetAllAsync();
            return Ok(GenericResponse<IEnumerable<Airport>>.SuccessResponse(airports));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse<Airport>>> GetAirport(Guid id)
        {
            var airport = await _airportService.GetByIdAsync(id);
            if (airport == null)
                return NotFound(GenericResponse<Airport>.ErrorResponse($"Airport with id {id} not found."));
            return Ok(GenericResponse<Airport>.SuccessResponse(airport));
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<Airport>>> CreateAirport(Airport airport)
        {
            var created = await _airportService.CreateAsync(airport);
            return CreatedAtAction(nameof(GetAirport), new { id = created.Id }, GenericResponse<Airport>.SuccessResponse(created, "Airport created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse<Airport>>> UpdateAirport(Guid id, Airport airport)
        {
            var updated = await _airportService.UpdateAsync(id, airport);
            if (updated == null)
                return BadRequest(GenericResponse<Airport>.ErrorResponse("Id mismatch or airport not found."));
            return Ok(GenericResponse<Airport>.SuccessResponse(updated, "Airport updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponse<string>>> DeleteAirport(Guid id)
        {
            var deleted = await _airportService.DeleteAsync(id);
            if (!deleted)
                return NotFound(GenericResponse<string>.ErrorResponse($"Airport with id {id} not found."));
            return Ok(GenericResponse<string>.SuccessResponse(null, "Airport deleted successfully."));
        }
    }
}
