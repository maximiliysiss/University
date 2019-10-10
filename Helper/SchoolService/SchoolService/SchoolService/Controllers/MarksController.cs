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
    [Route("api/[controller]")]
    [AuthorizeAttribute(Roles = "Teacher")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MarksController(DatabaseContext context)
        {
            _context = context;
        }

        public Teacher Teacher
        {
            get
            {
                var user = this.GetCurrentUser(_context);
                return _context.Teachers.Find(user.ID);
            }
        }

        // GET: api/Marks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mark>>> GetMarks()
        {
            var teacherClasses = Teacher.Class.Select(x => x.ID);
            var schedules = _context.Schedules.Where(x => teacherClasses.Contains(x.ID)).Select(x => x.ID);
            return await _context.Marks.Where(x => schedules.Contains(x.Schedule.ID)).ToListAsync();
        }

        // GET: api/Marks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mark>> GetMark(int id)
        {
            var mark = await _context.Marks.FindAsync(id);

            if (mark == null || !Teacher.Class.Select(x => x.ID).Contains(mark.Schedule.ClassId))
            {
                return NotFound();
            }

            return mark;
        }

        // PUT: api/Marks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMark(int id, Mark mark)
        {
            if (id != mark.ID || !Teacher.Class.Select(x => x.ID).Contains(_context.Marks.Find(id).Schedule.ClassId))
            {
                return BadRequest();
            }

            _context.Entry(mark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Marks
        [HttpPost]
        public async Task<ActionResult<Mark>> PostMark(Mark mark)
        {
            _context.Marks.Add(mark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMark", new { id = mark.ID }, mark);
        }

        // DELETE: api/Marks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Mark>> DeleteMark(int id)
        {
            var mark = await _context.Marks.FindAsync(id);
            if (mark == null || !Teacher.Class.Select(x => x.ID).Contains(mark.Schedule.ClassId))
            {
                return NotFound();
            }

            _context.Marks.Remove(mark);
            await _context.SaveChangesAsync();

            return mark;
        }

        private bool MarkExists(int id)
        {
            return _context.Marks.Any(e => e.ID == id);
        }
    }
}
