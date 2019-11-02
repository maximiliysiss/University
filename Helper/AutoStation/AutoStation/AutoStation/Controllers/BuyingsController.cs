using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoStation.Models;
using AutoStation.Models.Controller;
using AutoStation.Services;
using Microsoft.AspNetCore.Authorization;

namespace AutoStation.Controllers
{
    /// <summary>
    /// Покупки
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BuyingsController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly DatabaseContext _context;

        public BuyingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Buyings
        /// <summary>
        /// Получить покупки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Buying>>> GetBuyings()
        {
            return await _context.Buyings.ToListAsync();
        }

        // POST: api/Buyings
        /// <summary>
        /// Добавить покупки
        /// </summary>
        /// <param name="buying"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Buying>> PostBuying(Buying buying)
        {
            var schedule = _context.Schedules.FirstOrDefault(x => x.ID == buying.ScheduleId);
            if (schedule == null)
                return NotFound();
            buying.Sum = schedule.Price * buying.Count;
            buying.HistorySchedule = $"{schedule.From.Name} - {schedule.To.Name} / {schedule.Time} / {schedule.DayOfWeek.ToString()} / {schedule.Price}";
            _context.Buyings.Add(buying);
            await _context.SaveChangesAsync();

            return buying;
        }
    }
}
