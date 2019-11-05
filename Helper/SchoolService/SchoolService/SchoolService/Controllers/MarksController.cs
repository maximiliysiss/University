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
    [Authorize(Roles = "Teacher, Admin")]
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

        [HttpGet("class/{id}")]
        public async Task<ActionResult<List<Mark>>> GetMarksByDate(int id, int day, int month, int year)
        {
            var user = this.GetCurrentUser(_context);
            var date = new DateTime(year, month, day);
            return await _context.Marks.Where(x => x.Date.Date == date && x.Schedule.ClassId == id
                                                && x.Schedule.TeacherId == user.ID).ToListAsync();
        }

        [HttpGet("myclass/{id}")]
        public async Task<ActionResult<List<Mark>>> GetMyClassMarksByDate(int id, int day, int month, int year)
        {
            var user = this.GetCurrentUser(_context);
            var date = new DateTime(year, month, day);
            return await _context.Marks.Where(x => x.Schedule.ClassId == id && x.Date.Date == date).ToListAsync();
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
        public async Task<ActionResult<Mark>> PutMark(int id, Mark mark)
        {
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

            return mark;
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
