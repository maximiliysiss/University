using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkerPluginAPI.Extensions;
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
        private readonly IStateService stateService;

        public WorkerChecksController(DatabaseContext context, IStateService stateService)
        {
            _context = context;
            this.stateService = stateService;
        }

        private Worker Worker => _context.Workers.FirstOrDefault(x => x.ID == int.Parse(this.User.Claims.FirstOrDefault(y => y.Type == "UserIdentifier").Value));

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<WorkerCheck>>> GetWorkerChecks() => await _context.WorkerChecks.Include(x => x.Worker).ToListAsync();

        [HttpGet("action")]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult<WorkerCheck>> ActionStatus()
        {
            var user = Worker;
            if (user == null)
                return NotFound();
            return await stateService.NextStateAsync(user.ID);
        }

        [Authorize(Roles = "Worker")]
        [HttpGet("status")]
        public async Task<ActionResult<WorkerCheck>> CurrentState()
        {
            var user = Worker;
            if (user == null)
                return NotFound();
            return await stateService.CurrentStateAsync(user.ID);
        }

        [Authorize(Roles = "Worker")]
        [HttpGet("pause")]
        public async Task<ActionResult<WorkerCheck>> Pause()
        {
            var user = Worker;
            if (user == null)
                return NotFound();
            return await stateService.PauseAsync(user.ID);
        }

        [HttpGet("info/{id}/{year}/{month}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WorkerInfo<DayInfo>>> GetWorkerCheckInfo(int id, int year, int month) => await stateService.GetWorkerDaysInfoAsync(id, year, month);
    }
}
