using AutoMapper;
using EventManagement.API.Dto;
using EventManagement.Core;
using EventManagement.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public EventsController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _repository.Events.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EventDto>>(events));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Event entity = _mapper.Map<Event>(model);
            await _repository.Events.Create(entity);
            await _repository.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EventDto model)
        {
            var eventToUpdate = await _repository.Events.GetById(id);

            if (eventToUpdate == null)
                return NotFound();

            var entity = _mapper.Map(model, eventToUpdate);
            await _repository.Events.Update(entity);
            await _repository.SaveChangesAsync();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventToDelete = await _repository.Events.GetById(id);
            if (eventToDelete == null)
                return NotFound();

            await _repository.Events.Delete(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/rsvp")]
        public async Task<IActionResult> RSVP(int id, [FromBody] string username)
        {
            var eventToUpdate = await _repository.Events.GetById(id);
            if (eventToUpdate == null)
                return NotFound();

            if (eventToUpdate.Attendees.Contains(username))
                return BadRequest("User already RSVP'd");

            if (eventToUpdate.Attendees.Count >= eventToUpdate.MaxAttendees)
                return BadRequest("Event is full");

            eventToUpdate.Attendees.Add(username);
            await _repository.SaveChangesAsync();

            return Ok("RSVP successful");
        }
    }

}
