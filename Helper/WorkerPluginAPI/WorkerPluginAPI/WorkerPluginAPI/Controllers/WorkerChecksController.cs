using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkerPluginAPI.Models;
using WorkerPluginAPI.Models.Controllers;
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
            var newCheck = (await _context.WorkerChecks.AsNoTracking().Where(x => x.WorkerId == user.ID).OrderByDescending(x => x.ID).FirstOrDefaultAsync()) ??
                                                                                                        new WorkerCheck { Type = Models.Type.Out, WorkerId = user.ID };
            newCheck.ID = 0;
            if (newCheck.Type == Models.Type.Pause)
            {
                newCheck.Type = Models.Type.Out;
                newCheck.DateTime = new DateTime(DateTime.Now.Ticks - newCheck.DateTime.Ticks);
            }
            else
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
            return await _context.WorkerChecks.Where(x => x.WorkerId == user.ID).OrderByDescending(x => x.ID).FirstOrDefaultAsync() ?? new WorkerCheck { Type = Models.Type.Out }; ;
        }

        [Authorize(Roles = "Worker")]
        [HttpGet("pause")]
        public async Task<ActionResult<WorkerCheck>> Pause()
        {
            var user = Worker;
            if (user == null)
                return NotFound();
            var lastAction = await _context.WorkerChecks.Where(x => x.WorkerId == user.ID && (x.Type == Models.Type.In || x.Type == Models.Type.Out)).OrderByDescending(x => x.ID)
                                                                                                                                                            .FirstOrDefaultAsync();
            if (lastAction == null || lastAction.Type != Models.Type.In)
                return BadRequest();
            var newAction = new WorkerCheck { Type = Models.Type.Pause, WorkerId = user.ID };
            newAction.DateTime = new DateTime(newAction.DateTime.Ticks - lastAction.DateTime.Ticks);
            _context.Add(newAction);
            _context.SaveChanges();
            return newAction;
        }

        [HttpGet("{id}/info")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WorkerInfo>> GetWorkerChecks(int id)
        {
            var worker = _context.Workers.FirstOrDefault(x => x.ID == id);
            if (worker == null)
                return NotFound();
            var workerInfo = new WorkerInfo { Worker = worker };
            var workJobs = await _context.WorkerChecks.Where(x => x.WorkerId == id).ToListAsync();
            for (int i = 1; i < workJobs.Count; i += 2)
            {
                var end = workJobs[i].Type == Models.Type.Pause ? new DateTime(workJobs[i].DateTime.Ticks + workJobs[i - 1].DateTime.Ticks) : workJobs[i].DateTime;
                workerInfo.WorkDistances.Add(new WorkDistance { Start = workJobs[i - 1].DateTime, End = end, StartType = workJobs[i - 1].Type, EndType = workJobs[i].Type });
            }

            if (workJobs.Count % 2 == 1)
                workerInfo.WorkDistances.Add(new WorkDistance { Start = workJobs.Last().DateTime, StartType = workJobs.Last().Type });

            return workerInfo;
        }
    }
}
