using Microsoft.AspNetCore.Mvc;
using StationaryClash.Interfaces.Services;
using StationaryClash.Models;

namespace StationaryClash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Dependency injection
        private readonly IUserService _userService;

        // Constructor
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.AuthAsync(request.Username, request.Password);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new RegisterResponse
                {
                    Success = false,
                    Message = "Username and password are required."
                });
            }

            try
            {
                bool result = await _userService.RegisterAsync(request.Username, request.Password);

                if (result)
                {
                    return Ok(new RegisterResponse
                    {
                        Success = true,
                        Message = "Registration successful."
                    });
                }
                else
                {
                    return Conflict(new RegisterResponse
                    {
                        Success = false,
                        Message = "Username already exists."
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, new RegisterResponse
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }
    } 
}
