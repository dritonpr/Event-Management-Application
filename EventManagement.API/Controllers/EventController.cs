using EventManagement.Core;
using EventManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly EventManagementDbContext _context;

        public EventController(EventManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event eventRequest)
        {
            _context.Events.Add(eventRequest);
            await _context.SaveChangesAsync();
            return Ok(eventRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event eventRequest)
        {
            var eventToUpdate = await _context.Events.FindAsync(id);
            if (eventToUpdate == null)
                return NotFound();

            eventToUpdate.Name = eventRequest.Name;
            eventToUpdate.Description = eventRequest.Description;
            eventToUpdate.Date = eventRequest.Date;
            eventToUpdate.Location = eventRequest.Location;
            eventToUpdate.MaxAttendees = eventRequest.MaxAttendees;

            await _context.SaveChangesAsync();
            return Ok(eventToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete == null)
                return NotFound();

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/rsvp")]
        public async Task<IActionResult> RSVP(int id, [FromBody] string username)
        {
            var eventToUpdate = await _context.Events.FindAsync(id);
            if (eventToUpdate == null)
                return NotFound();

            if (eventToUpdate.Attendees.Contains(username))
                return BadRequest("User already RSVP'd");

            if (eventToUpdate.Attendees.Count >= eventToUpdate.MaxAttendees)
                return BadRequest("Event is full");

            eventToUpdate.Attendees.Add(username);
            await _context.SaveChangesAsync();

            return Ok("RSVP successful");
        }
    }

}
