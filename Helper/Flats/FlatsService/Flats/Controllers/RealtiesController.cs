using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flats.Models;
using Flats.Services;

namespace Flats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealtiesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RealtiesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Realties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Realty>>> GetRealties()
        {
            return await _context.Realties.ToListAsync();
        }

        // GET: api/Realties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Realty>> GetRealty(int id)
        {
            var realty = await _context.Realties.FindAsync(id);

            if (realty == null)
            {
                return NotFound();
            }

            return realty;
        }

        // PUT: api/Realties/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Realty>> PutRealty(int id, Realty realty)
        {
            if (id != realty.ID)
            {
                return BadRequest();
            }

            _context.Entry(realty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealtyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return realty;
        }

        // POST: api/Realties
        [HttpPost]
        public async Task<ActionResult<Realty>> PostRealty(Realty realty)
        {
            _context.Realties.Add(realty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRealty", new { id = realty.ID }, realty);
        }

        // DELETE: api/Realties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Realty>> DeleteRealty(int id)
        {
            var realty = await _context.Realties.FindAsync(id);
            if (realty == null)
            {
                return NotFound();
            }

            _context.Realties.Remove(realty);
            await _context.SaveChangesAsync();

            return realty;
        }

        private bool RealtyExists(int id)
        {
            return _context.Realties.Any(e => e.ID == id);
        }
    }
}
