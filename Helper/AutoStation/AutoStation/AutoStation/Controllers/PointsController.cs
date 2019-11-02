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
    /// <summary>
    /// Точик
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PointsController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly DatabaseContext _context;

        public PointsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Points
        /// <summary>
        /// Получить список
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Point>>> GetPoints()
        {
            return await _context.Points.ToListAsync();
        }

        // GET: api/Points/5
        /// <summary>
        /// Получить точку
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="id"></param>
        /// <param name="point"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Point>> PostPoint(Point point)
        {
            _context.Points.Add(point);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoint", new { id = point.ID }, point);
        }

        // DELETE: api/Points/5
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Существует ли точка
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool PointExists(int id)
        {
            return _context.Points.Any(e => e.ID == id);
        }
    }
}
