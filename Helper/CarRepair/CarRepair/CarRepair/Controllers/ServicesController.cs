using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRepair.Models;
using CarRepair.Services;
using Microsoft.AspNetCore.Authorization;

namespace CarRepair.Controllers
{
    /// <summary>
    /// Контроллер для услуг
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ServicesController(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить услуги
        /// </summary>
        /// <returns></returns>
        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        /// <summary>
        /// Получить услугу по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        /// <summary>
        /// Изменить услугу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        // PUT: api/Services/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Service>> PutService(int id, Service service)
        {
            if (id != service.ID)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return service;
        }

        /// <summary>
        /// Добавить услугу
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        // POST: api/Services
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.ID }, service);
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Service>> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return service;
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ID == id);
        }
    }
}
