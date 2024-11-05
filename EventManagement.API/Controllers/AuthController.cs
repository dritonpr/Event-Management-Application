using EventManagement.API.Services;
using EventManagement.Common.Dto;
using EventManagement.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
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
        
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            if (await _authService.Register(model) == null)
            {
                return BadRequest("User registration failed.");
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var token = await _authService.Login(model);
            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { Token = token });
        }
    }
}
