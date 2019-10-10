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
            return await _context.Marks.Where(x => x.TeacherId == Teacher.ID).ToListAsync();
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
            using (var userContext = this.GetUserContext())
            {
                var markClass = userContext.DatabaseContext.Marks.FirstOrDefault(x => x.ID == id)?.Schedule?.Class;
                if (id != mark.ID || !(userContext.User as Teacher).Class.Select(x => x.ID).Contains(markClass.ID))
                    return BadRequest();
                mark.TeacherId = userContext.User.ID;
            }

            _context.Entry(mark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Marks
        [HttpPost]
        public async Task<ActionResult<Mark>> PostMark(Mark mark)
        {
            mark.TeacherId = this.GetCurrentUser(_context).ID;
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
                return NotFound();

            _context.Marks.Remove(mark);
            await _context.SaveChangesAsync();

            return mark;
        }

        private bool MarkExists(int id) => _context.Marks.Any(e => e.ID == id);
    }
}
