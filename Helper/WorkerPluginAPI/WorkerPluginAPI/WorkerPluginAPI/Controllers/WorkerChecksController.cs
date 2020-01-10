using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkerPluginAPI.Models;
using WorkerPluginAPI.Services;

namespace WorkerPluginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerChecksController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public WorkerChecksController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/WorkerChecks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerCheck>>> GetWorkerChecks()
        {
            return await _context.WorkerChecks.ToListAsync();
        }

        // POST: api/WorkerChecks
        [HttpPost]
        public async Task<ActionResult<WorkerCheck>> PostWorkerCheck(WorkerCheck workerCheck)
        {
            _context.WorkerChecks.Add(workerCheck);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkerCheck", new { id = workerCheck.ID }, workerCheck);
        }

        [Authorize]
        [HttpGet("status")]
        public async Task<ActionResult<WorkerCheck>> CurrentState()
        {
            var user = await _context.Workers.FirstOrDefaultAsync(x => x.ID == int.Parse(this.User.Claims.FirstOrDefault(y => y.Type == ClaimsIdentity.DefaultNameClaimType).Value));
            if (user == null)
                return NotFound();
            return await _context.WorkerChecks.Where(x => x.WorkerId == user.ID).OrderByDescending(x => x.DateTime).FirstOrDefaultAsync();
        }
    }
}
