using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoStation.Models;
using AutoStation.Services;
using Microsoft.AspNetCore.Authorization;

namespace AutoStation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PointsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PointsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Points
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Point>>> GetPoints()
        {
            return await _context.Points.ToListAsync();
        }

        // GET: api/Points/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Point>> GetPoint(int id)
        {
            var point = await _context.Points.FindAsync(id);

            if (point == null)
            {
                return NotFound();
            }

            return point;
        }

        // PUT: api/Points/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Point>> PutPoint(int id, Point point)
        {
            if (id != point.ID)
            {
                return BadRequest();
            }

            _context.Entry(point).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return point;
        }

        // POST: api/Points
        [HttpPost]
        public async Task<ActionResult<Point>> PostPoint(Point point)
        {
            _context.Points.Add(point);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoint", new { id = point.ID }, point);
        }

        // DELETE: api/Points/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Point>> DeletePoint(int id)
        {
            var point = await _context.Points.FindAsync(id);
            if (point == null)
            {
                return NotFound();
            }

            _context.Points.Remove(point);
            await _context.SaveChangesAsync();

            return point;
        }

        private bool PointExists(int id)
        {
            return _context.Points.Any(e => e.ID == id);
        }
    }
}
