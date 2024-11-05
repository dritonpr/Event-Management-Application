using AutoMapper;
using EventManagement.API.Services;
using EventManagement.Common.Dto;
using EventManagement.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IAuthService _authService;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IMapper _mapper;

        public AuthController(IRepositoryWrapper repository, IAuthService authService, IJwtAuthManager jwtAuthManager, IMapper mapper)
        {
            _repository = repository;
            _authService = authService;
            _jwtAuthManager = jwtAuthManager;
            _mapper = mapper;
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
            var user = await _authService.Login(model);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            string token = UserTokenData(_mapper.Map<UserDto>(user));

            return Ok(new { Token = token });
        }

        private string UserTokenData(UserDto user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            var jwtResult = _jwtAuthManager.GenerateTokens(user.Username, claims, DateTime.Now);
            return jwtResult.TokenString;
        }
    }
}
