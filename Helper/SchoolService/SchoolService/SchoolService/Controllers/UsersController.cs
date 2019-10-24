using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolService.Models;
using SchoolService.Services;
using SchoolService.Extensions;

namespace SchoolService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin, JobTeacher")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var currentUser = this.GetCurrentUser(_context);

            if (currentUser.UserType != UserType.Admin && currentUser.ID != id)
                return BadRequest();

            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, JobTeacher")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            if (id != user.ID || user.UserType == UserType.Student || user.UserType == UserType.Teacher)
                return BadRequest();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                else
                    throw;
            }

            return user;
        }

        // POST: api/Users
        [HttpPost]
        [Authorize(Roles = "Admin, JobTeacher")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (user.UserType == UserType.Student || user.UserType == UserType.Teacher || _context.Users.Any(x => x.Login == user.Login))
                return BadRequest();
            user.PasswordHash = CryptService.CreateMD5(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, JobTeacher")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private bool UserExists(int id) => _context.Users.Any(e => e.ID == id);
    }
}
