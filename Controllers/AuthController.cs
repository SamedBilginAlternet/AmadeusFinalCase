using AmadeusFlightApý.Models;
using AmadeusFlightApý.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmadeusFlightApý.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<GenericResponse<string>>> Register([FromBody] RegisterRequest request)
        {
            var user = await _userService.RegisterAsync(request.UserName, request.Password);
            if (user == null)
                return BadRequest(GenericResponse<string>.ErrorResponse("Username already exists."));
            return Ok(GenericResponse<string>.SuccessResponse(null, "Registration successful."));
        }

        [HttpPost("login")]
        public async Task<ActionResult<GenericResponse<string>>> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.AuthenticateAsync(request.UserName, request.Password);
            if (user == null)
                return Unauthorized(GenericResponse<string>.ErrorResponse("Invalid credentials."));
            var token = _jwtService.GenerateToken(user);
            return Ok(GenericResponse<string>.SuccessResponse(token, "Login successful."));
        }
    }

    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
