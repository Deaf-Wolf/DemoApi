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
            var evt = await _context.Events.ToListAsync();

            return Ok(evt);
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<Event>> GetEvent(Guid guid)
        {
            var evt = await _context.Events.FindAsync(guid);
            if (evt is null)
            {
                return NotFound("User not found");
            }

            return Ok(evt);
        }

        [HttpPost]
        public async Task<ActionResult<List<Event>>> AddEvent(Event evt)
        {

            evt.Id = Guid.NewGuid(); 
            await _context.Events.AddAsync(evt);
            await _context.SaveChangesAsync();

            return Ok(evt);
        }

        [HttpPut("{guid}")]
        public async Task<ActionResult<List<Event>>> UpdateEvent(Guid guid, Event evt)
        {
            var dbevt = await _context.Events.FindAsync(guid);
            if (dbevt is null)
            {
                return NotFound("User not found");
            }

            dbevt.Id = evt.Id;
            dbevt.Title = evt.Title;
            dbevt.EventDate = evt.EventDate;
            dbevt.StartTime = evt.StartTime;
            dbevt.EndTime = evt.EndTime;
            dbevt.Description = evt.Description;
            dbevt.Eventtype = evt.Eventtype;
            dbevt.UserId = evt.UserId;

            _context.Events.Update(dbevt);
            await _context.SaveChangesAsync();

            return Ok($"User {evt.Id} Updated");
        }

        [HttpDelete("{guid}")]
        public async Task<ActionResult<List<User>>> DeleteEvent(Guid guid)
        {
            var dbevt = await _context.Events.FindAsync(guid);
            if (dbevt is null)
            {
                return NotFound("User not found");
            }
            _context.Events.Remove(dbevt);
            await _context.SaveChangesAsync();

            return Ok($"User {dbevt.Title} Killed");
        }
    }
}
