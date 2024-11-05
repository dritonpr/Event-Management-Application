using AutoMapper;
using EventManagement.API.Services;
using EventManagement.Common.Dto;
using EventManagement.Core;
using EventManagement.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IMySessionService _mySessionService;

        public EventsController(IRepositoryWrapper repository, IMapper mapper, IMySessionService mySessionService)
        {
            _repository = repository;
            _mapper = mapper;
            _mySessionService = mySessionService;
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
        public async Task<IActionResult> Delete(int id)
        {
            var eventToDelete = await _repository.Events.GetById(id);
            if (eventToDelete == null)
                return NotFound();

            await _repository.Events.Delete(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(DateTime? eventDate = null, string category = null)
        {
            string username = _mySessionService.GetUsername();
            var events = await _repository.Events.GetByCondition(a => (eventDate.HasValue ? a.Date.Date == eventDate.Value.Date : true)
                                                                   && (!string.IsNullOrEmpty(category) ? a.Category == category : true))
                     .Select(c => new EventDto()
                     {
                         Attendees = c.Attendees,
                         Category = category,
                         CreatedByUserId = c.CreatedByUserId,
                         Date = c.Date,
                         Description = c.Description,
                         Id = c.Id,
                         Location = c.Location,
                         MaxAttendees = c.MaxAttendees,
                         Name = c.Name,
                         HasRespond = c.Attendees.Any(a => a == username),
                     }).ToListAsync();

            return Ok(events);
        }

        [HttpPut("{id}/rsvp")]
        public async Task<IActionResult> RSVP(int id)
        {
            var userName = _mySessionService.GetUsername();
            var events = await _repository.Events.GetById(id);
            if (events == null)
                return NotFound();

            if (events.MaxAttendees <= events.Attendees.Count())
                return BadRequest("Sorry, this event has reached its maximum number of attendees. You cannot respond to this event!");

            if (events.Attendees.Contains(userName))
                return BadRequest("You already responded to this event!");

            events.Attendees.Add(userName);
            await _repository.SaveChangesAsync();
            return Ok("Your respond has been confirmed successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.Events.GetByCondition(a => a.Id == id).FirstOrDefaultAsync();
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<EventDto>(entity));
        }
    }

}
