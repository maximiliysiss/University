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
    /// <summary>
    /// Работа с отметками о работе
    /// </summary>
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

        /// <summary>
        /// Получить работника по токену
        /// </summary>
        private Worker Worker => _context.Workers.FirstOrDefault(x => x.ID == int.Parse(this.User.Claims.FirstOrDefault(y => y.Type == "UserIdentifier").Value));

        /// <summary>
        /// Получить все отметки о работе (просто список)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<WorkerCheck>>> GetWorkerChecks() => await _context.WorkerChecks.Include(x => x.Worker).ToListAsync();

        /// <summary>
        /// Переход на след состояние
        /// </summary>
        /// <returns></returns>
        [HttpGet("action")]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult<WorkerCheck>> ActionStatus()
        {
            var user = Worker;
            if (user == null)
                return NotFound();
            return await stateService.NextStateAsync(user.ID);
        }

        /// <summary>
        /// Получить текущий статус
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Worker")]
        [HttpGet("status")]
        public async Task<ActionResult<WorkerCheck>> CurrentState()
        {
            var user = Worker;
            if (user == null)
                return NotFound();
            return await stateService.CurrentStateAsync(user.ID);
        }

        /// <summary>
        /// Пауза
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Worker")]
        [HttpGet("pause")]
        public async Task<ActionResult<WorkerCheck>> Pause()
        {
            var user = Worker;
            if (user == null)
                return NotFound();
            return await stateService.PauseAsync(user.ID);
        }

        /// <summary>
        /// Получить информацию о работнике на месяц
        /// </summary>
        /// <param name="id"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("info/{id}/{year}/{month}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WorkerInfo<DayInfo>>> GetWorkerCheckInfo(int id, int year, int month) => await stateService.GetWorkerDaysInfoAsync(id, year, month);
    }
}
