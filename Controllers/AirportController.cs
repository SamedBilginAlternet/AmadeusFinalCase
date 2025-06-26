using AmadeusFlightApý.Dtos;
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
        public async Task<ActionResult<GenericResponse<IEnumerable<AirportDto>>>> GetAirports()
        {
            var airports = await _airportService.GetAllAsync();
            return Ok(GenericResponse<IEnumerable<AirportDto>>.SuccessResponse(airports));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse<AirportDto>>> GetAirport(Guid id)
        {
            var airport = await _airportService.GetByIdAsync(id);
            if (airport == null)
                return NotFound(GenericResponse<AirportDto>.ErrorResponse($"Airport with id {id} not found."));
            return Ok(GenericResponse<AirportDto>.SuccessResponse(airport));
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<AirportDto>>> CreateAirport([FromBody] CreateAirportDto dto)
        {
            var created = await _airportService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAirport), new { id = created.Id }, GenericResponse<AirportDto>.SuccessResponse(created, "Airport created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponse<AirportDto>>> UpdateAirport(Guid id, [FromBody] UpdateAirportDto dto)
        {
            var updated = await _airportService.UpdateAsync(id, dto);
            if (updated == null)
                return BadRequest(GenericResponse<AirportDto>.ErrorResponse("Id mismatch or airport not found."));
            return Ok(GenericResponse<AirportDto>.SuccessResponse(updated, "Airport updated successfully."));
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
