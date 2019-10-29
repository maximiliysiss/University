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

namespace SchoolService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public LessonsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            return await _context.Lessons.ToListAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }

        // PUT: api/Lessons/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutLesson(int id, Lesson lesson)
        {
            if (id != lesson.ID)
            {
                return BadRequest();
            }

            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
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

        // POST: api/Lessons
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLesson", new { id = lesson.ID }, lesson);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Lesson>> DeleteLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return lesson;
        }

        [HttpGet("{id}/{teacherId}")]
        [Authorize(Roles = "Admin, JobTeacher")]
        public async Task<ActionResult<LessonProfile>> SetLessonProfile(int id, int teacherId)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(x => x.ID == teacherId);
            Lesson lesson = _context.Lessons.FirstOrDefault(x => x.ID == id);
            if (lesson == null || teacher == null)
                return NotFound();
            LessonProfile lessonProfile = await _context.LessonProfiles.FirstOrDefaultAsync(x => x.TeacherId == teacherId && x.LessonId == id);
            if (lessonProfile == null)
            {
                lessonProfile = new LessonProfile
                {
                    Lesson = lesson,
                    Teacher = teacher
                };
                _context.Add(lessonProfile);
            }
            else
                _context.Remove(lessonProfile);
            _context.SaveChanges();
            return lessonProfile;
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.ID == id);
        }
    }
}
