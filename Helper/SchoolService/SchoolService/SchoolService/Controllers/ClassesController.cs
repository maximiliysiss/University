using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolService.Extensions;
using SchoolService.Models;
using SchoolService.Services;

namespace SchoolService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ClassesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            var userContext = this.GetCurrentUser(_context);
            if (userContext.UserType != UserType.Teacher)
                return await _context.Classes.ToListAsync();
            return _context.Teachers.Find(userContext.ID).Class;
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            var userContext = this.GetCurrentUser(_context);
            var teacher = userContext as Teacher;
            if (@class == null || (userContext.UserType == UserType.Teacher && !_context.Teachers.Find(userContext.ID).Class.Select(x => x.ID).Contains(id)))
                return NotFound();
            return @class;
        }

        // PUT: api/Classes/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<ActionResult<Class>> PutClass(int id, Class @class)
        {
            using (var userContext = this.GetUserContext())
            {
                if (id != @class.ID || (!(userContext.User as Teacher)?.Class.Select(x => x.ID).Contains(id) ?? false))
                    return BadRequest();
            }

            _context.Entry(@class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
                    return NotFound();
                else
                    throw;
            }

            return @class;
        }

        // POST: api/Classes
        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            Teacher teacher = this.GetCurrentUser(_context) as Teacher;
            @class.TeacherId = teacher.ID;
            teacher.IsClassWork = true;
            _context.Update(teacher);
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @class.ID }, @class);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<ActionResult<Class>> DeleteClass(int id)
        {
            var user = this.GetCurrentUser(_context);
            var teacher = user as Teacher;
            var @class = await _context.Classes.FindAsync(id);

            if (@class == null || (!teacher?.Class.Select(x => x.ID).Contains(@class.ID) ?? false))
                return NotFound();

            teacher.IsClassWork = teacher.Class.Count <= 1;
            _context.Classes.Remove(@class);
            _context.Update(teacher);
            await _context.SaveChangesAsync();

            return @class;
        }

        [HttpGet("teacher/{id}")]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<ActionResult> selectClass(int id)
        {
            var teacher = this.GetCurrentUser(_context) as Teacher;
            var _class = _context.Classes.FirstOrDefault(x => x.ID == id);
            if (_class == null)
                return NotFound();
            _class.Teacher = teacher;
            _context.Update(_class);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ID == id);
        }
    }
}
