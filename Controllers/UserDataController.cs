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
    public class UserDataController : ControllerBase
    {
        private readonly DataContext _context;

        public UserDataController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<User>> GetUser(Guid guid)
        {
            var user = await _context.Users.FindAsync(guid);
            if (user is null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {

            user.Guid = Guid.NewGuid(); 
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("{guid}")]
        public async Task<ActionResult<List<User>>> UpdateUser(Guid guid, User user)
        {
            var dbUser = await _context.Users.FindAsync(guid);
            if (dbUser is null)
            {
                return NotFound("User not found");
            }

            dbUser.UserName = user.UserName;
            dbUser.Email = user.Email;
            dbUser.Password = user.Password;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;

            _context.Users.Update(dbUser);
            await _context.SaveChangesAsync();

            return Ok($"User {user.Guid} Updated");
        }

        [HttpDelete("{guid}")]
        public async Task<ActionResult<List<User>>> DeleteUser(Guid guid)
        {
            var dbUser = await _context.Users.FindAsync(guid);
            if (dbUser is null)
            {
                return NotFound("User not found");
            }
            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok($"User {dbUser.UserName} Killed");
        }
    }
}
