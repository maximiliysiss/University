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
    [AuthorizeAttribute(Roles = "Admin, KnowledgeTeacher")]
    public class ChildrenController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ChildrenController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Children
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Child>>> GetChildren()
        {
            return await _context.Children.Where(x => !x.IsArchive).ToListAsync();
        }

        // GET: api/Children/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Child>> GetChild(int id)
        {
            var child = await _context.Children.FindAsync(id);

            if (child == null)
            {
                return NotFound();
            }

            return child;
        }

        // PUT: api/Children/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChild(int id, Child child)
        {
            if (id != child.ID)
                return BadRequest();

            _context.Entry(child).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // GET: api/3/class/3
        [HttpGet("{id}/class/{class}")]
        public async Task<ActionResult<Child>> ChangeClass(int id, int @class)
        {
            var child = _context.Children.FirstOrDefault(x => x.ID == id);
            var cl = _context.Classes.FirstOrDefault(x => x.ID == @class);

            if (child == null || cl == null)
                return NotFound();

            child.Class = cl;
            _context.Entry(child).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return child;
        }

        // POST: api/Children
        [HttpPost]
        public async Task<ActionResult<Child>> PostChild(Child child)
        {
            if (_context.Children.Any(x => x.Login == child.Login))
                return BadRequest();

            child.PasswordHash = CryptService.CreateMD5(child.PasswordHash);
            _context.Children.Add(child);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChild", new { id = child.ID }, child);
        }

        // DELETE: api/Children/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Child>> DeleteChild(int id)
        {
            var child = await _context.Children.FindAsync(id);
            if (child == null)
                return NotFound();

            _context.Children.Remove(child);
            await _context.SaveChangesAsync();

            return child;
        }

        private bool ChildExists(int id)
        {
            return _context.Children.Any(e => e.ID == id);
        }

        [HttpGet("{id}/archive")]
        public ActionResult<Child> Archive(int id)
        {
            var child = _context.Children.FirstOrDefault(x => x.ID == id);
            if (child == null)
                return NotFound();
            child.IsArchive = !child.IsArchive;
            _context.Update(child);
            _context.SaveChanges();
            return child;
        }
    }
}
