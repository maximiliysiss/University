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
    /// <summary>
    /// Контроллер для недвижимости
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RealtiesController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly DatabaseContext _context;

        public RealtiesController(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить список
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Realty>>> GetRealties()
        {
            return await _context.Realties.ToListAsync();
        }

        /// <summary>
        /// Получить элемент по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="id"></param>
        /// <param name="realty"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="realty"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Realty>> PostRealty(Realty realty)
        {
            _context.Realties.Add(realty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRealty", new { id = realty.ID }, realty);
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Проверка на существование недвижимости
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool RealtyExists(int id)
        {
            return _context.Realties.Any(e => e.ID == id);
        }
    }
}
