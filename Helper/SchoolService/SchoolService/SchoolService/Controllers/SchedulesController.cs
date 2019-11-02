using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SchoolService.Extensions;
using SchoolService.Models;
using SchoolService.Services;

namespace SchoolService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchedulesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SchedulesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Schedules
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            Task<List<Schedule>> res = null;
            var user = this.GetCurrentUser(_context);
            switch (user.UserType)
            {
                case UserType.Admin:
                case UserType.JobTeacher:
                case UserType.KnowledgeTeacher:
                    res = _context.Schedules.ToListAsync();
                    break;
                case UserType.Teacher:
                    {
                        var classes = _context.Teachers.Find(user.ID).Class.Select(x => x.ID);
                        res = _context.Schedules.Where(x => classes.Contains(x.ClassId)).ToListAsync();
                    }
                    break;
                case UserType.Student:
                    {
                        var c = _context.Children.Find(user.ID)?.Class?.ID;
                        if (c == null)
                            return new List<Schedule>();
                        res = _context.Schedules.Where(x => x.ClassId == c).ToListAsync();
                    }
                    break;
            }

            return await res;
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }

        [HttpGet("{id}/facultative")]
        [Authorize(Roles = "KnowledgeTeacher, Admin, JobTeacher")]
        public async Task<ActionResult<Schedule>> MakeFacultative(int id)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(x => x.ID == id);
            if (schedule == null)
                return NotFound();
            schedule.IsFacultative = !schedule.IsFacultative;
            _context.Update(schedule);
            _context.SaveChanges();
            return schedule;
        }

        // PUT: api/Schedules/5
        [HttpPut("{id}")]
        [Authorize(Roles = "KnowledgeTeacher, Admin, JobTeacher")]
        public async Task<ActionResult<Schedule>> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.ID)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return schedule;
        }

        // POST: api/Schedules
        [HttpPost]
        [Authorize(Roles = "KnowledgeTeacher, Admin, JobTeacher")]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.ID }, schedule);
        }

        [HttpGet("class/{day}/{class}")]
        public ActionResult<List<Schedule>> GetScheduleByDayAndClass(int day, int @class)=>_context.Schedules.Where(x=>(int)x.DayOfWeek==day && x.ClassId == @class).ToList();

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "KnowledgeTeacher, Admin, JobTeacher")]
        public async Task<ActionResult<Schedule>> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return schedule;
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.ID == id);
        }
    }
}
