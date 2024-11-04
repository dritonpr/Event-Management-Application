using EventManagement.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public UserController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(DateTime? eventDate = null, string category = null)
        {
            var users = await _repository.User.GetAll().ToListAsync();
            return Ok(users);
        }
    }
}
