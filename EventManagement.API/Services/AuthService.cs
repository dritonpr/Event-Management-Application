using EventManagement.API.Dto;
using EventManagement.Core;
using EventManagement.Data.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace EventManagement.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepositoryWrapper repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
     
        public async Task<User> Register(UserRegisterDto model)
        {
            CreatePasswordHash(model.Password, out string passwordHash, out string passwordSalt);
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _repository.User.Create(user);
            await _repository.SaveChangesAsync();
            return user;
        }

        public async Task<string> Login(UserLoginDto model)
        {
            var user = await _repository.User.GetByCondition(a => a.Username == model.Username).FirstOrDefaultAsync();
            if (user == null || !VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return CreateToken(user);
        }

        private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            using var hmac = new HMACSHA512(Convert.FromBase64String(passwordSalt));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(Convert.FromBase64String(passwordHash));
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
    public interface IAuthService
    {
        Task<User> Register(UserRegisterDto userRegisterDto);
        Task<string> Login(UserLoginDto userLoginDto);
    }
}
