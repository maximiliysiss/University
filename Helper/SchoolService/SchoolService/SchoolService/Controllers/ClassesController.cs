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
            return _context.Classes.Where(x => x.TeacherId == userContext.ID).ToList();
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

        [HttpGet("{id}/marks")]
        public ActionResult<List<Mark>> GetClassMarks(int id)
        {
            return _context.Marks.FromSql(@"select m.* from Marks m
                                            left join Schedules on m.ScheduleId = Schedules.ID
                                            where Schedules.ClassId = {0}
                                            order by Schedules.DayOfWeek, Schedules.LessonNumber", id).ToList();
        }

        // PUT: api/Classes/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, KnowledgeTeacher")]
        public async Task<ActionResult<Class>> PutClass(int id, Class @class)
        {
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
        [Authorize(Roles = "KnowledgeTeacher, Admin")]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            var teacher = _context.Teachers.FirstOrDefault(x => x.ID == @class.TeacherId);
            if (teacher == null)
                return NotFound();
            teacher.IsClassWork = true;
            _context.Update(teacher);
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @class.ID }, @class);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "KnowledgeTeacher, Admin")]
        public async Task<ActionResult<Class>> DeleteClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
                return NotFound();

            @class.Teacher.IsClassWork = @class.Teacher.Class.Count - 1 > 0;

            _context.Classes.Remove(@class);
            _context.Update(@class.Teacher);
            await _context.SaveChangesAsync();

            return @class;
        }

        [HttpGet("teacher/{id}/{teacherId}")]
        [Authorize(Roles = "KnowledgeTeacher, Admin, JobTeacher")]
        public async Task<ActionResult> selectClass(int id, int teacherId)
        {
            var _class = _context.Classes.FirstOrDefault(x => x.ID == id);
            var teacher = _context.Teachers.FirstOrDefault(x => x.ID == teacherId);
            if (_class == null || teacher == null)
                return NotFound();
            _class.TeacherId = teacherId;
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
