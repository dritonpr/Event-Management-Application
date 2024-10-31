using EventManagement.API.Dto;
using EventManagement.API.Services;
using EventManagement.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IAuthService _authService;

        public AuthController(IRepositoryWrapper repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            if (await _authService.Register(request) == null)
            {
                return BadRequest("User registration failed.");
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var token = await _authService.Login(request);
            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { Token = token });
        }
    }
}
