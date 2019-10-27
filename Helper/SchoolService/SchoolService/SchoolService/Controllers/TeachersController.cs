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
    [ApiController]
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TeachersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        [AuthorizeAttribute(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        [HttpGet("{id}")]
        [AuthorizeAttribute(Roles = "Admin, Teacher")]
        public ActionResult<Teacher> GetTeacher(int id) => RedirectToAction("GetUser", "Users", new { id = id });

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        [AuthorizeAttribute(Roles = "Admin")]
        public async Task<ActionResult<Teacher>> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.ID)
                return BadRequest();

            teacher.PasswordHash = CryptService.CreateMD5(teacher.PasswordHash);
            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                    return NotFound();
                else
                    throw;
            }

            return teacher;
        }

        // POST: api/Teachers
        [HttpPost]
        [AuthorizeAttribute(Roles = "Admin")]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            teacher.PasswordHash = CryptService.CreateMD5(teacher.PasswordHash);
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", "Users", new { id = teacher.ID }, teacher);
        }

        private bool TeacherExists(int id) => _context.Teachers.Any(e => e.ID == id);
    }
}
