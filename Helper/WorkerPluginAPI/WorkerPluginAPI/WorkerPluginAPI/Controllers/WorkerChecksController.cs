using System;
using System.Collections.Generic;
using System.Linq;
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

        private Worker Worker => _context.Workers.FirstOrDefault(x => x.ID == int.Parse(this.User.Claims.FirstOrDefault(y => y.Type == "UserIdentifier").Value));

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<WorkerCheck>>> GetWorkerChecks()
        {
            return await _context.WorkerChecks.Include(x => x.Worker).ToListAsync();
        }

        [HttpGet("action")]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult<WorkerCheck>> ActionStatus()
        {
            var user = Worker;
            if (user == null)
                return NotFound();
            var newCheck = (await _context.WorkerChecks.AsNoTracking().Where(x => x.WorkerId == user.ID).OrderByDescending(x => x.DateTime).FirstOrDefaultAsync()) ??
                                                                                                        new WorkerCheck { Type = Models.Type.In, WorkerId = user.ID };
            newCheck.ID = 0;
            newCheck.DateTime = DateTime.Now;
            newCheck.Type = (Models.Type)(((int)newCheck.Type + 1) % 2);
            _context.Add(newCheck);
            _context.SaveChanges();
            return newCheck;
        }

        [Authorize(Roles = "Worker")]
        [HttpGet("status")]
        public async Task<ActionResult<WorkerCheck>> CurrentState()
        {
            var user = Worker;
            if (user == null)
                return NotFound();
            return await _context.WorkerChecks.Where(x => x.WorkerId == user.ID).OrderByDescending(x => x.DateTime).FirstOrDefaultAsync() ?? new WorkerCheck { Type = Models.Type.Out };
        }
    }
}
