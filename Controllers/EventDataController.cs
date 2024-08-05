using DemoApi.data;
using DemoApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;
using System;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDataController : ControllerBase
    {
        private readonly DataContext _context;

        public EventDataController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            var events = await _context.Events.ToListAsync();

            return Ok(events);
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<Event>> GetEvent(Guid guid)
        {
            var events = await _context.Events.FindAsync(guid);
            if (events is null)
            {
                return NotFound("User not found");
            }

            return Ok(events);
        }

        [HttpPost]
        public async Task<ActionResult<List<Event>>> AddEvent(Event events)
        {

            events.Id = Guid.NewGuid(); 
            await _context.Events.AddAsync(events);
            await _context.SaveChangesAsync();

            return Ok(events);
        }

        [HttpPut("{guid}")]
        public async Task<ActionResult<List<Event>>> UpdateEvent(Guid guid, Event events)
        {
            var dbevents = await _context.Events.FindAsync(guid);
            if (dbevents is null)
            {
                return NotFound("User not found");
            }

            dbevents.Id = events.Id;
            dbevents.Title = events.Title;
            dbevents.EventDate = events.EventDate;
            dbevents.StartTime = events.StartTime;
            dbevents.EndTime = events.EndTime;
            dbevents.Description = events.Description;
            dbevents.Eventtype = events.Eventtype;
            dbevents.UserId = events.UserId;

            _context.Events.Update(dbevents);
            await _context.SaveChangesAsync();

            return Ok($"User {events.Id} Updated");
        }

        [HttpDelete("{guid}")]
        public async Task<ActionResult<List<User>>> DeleteEvent(Guid guid)
        {
            var dbevents = await _context.Events.FindAsync(guid);
            if (dbevents is null)
            {
                return NotFound("User not found");
            }
            _context.Events.Remove(dbevents);
            await _context.SaveChangesAsync();

            return Ok($"User {dbevents.Title} Killed");
        }
    }
}
