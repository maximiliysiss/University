using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoStation.Models;
using AutoStation.Services;

namespace AutoStation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BuyingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Buyings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buying>>> GetBuyings()
        {
            return await _context.Buyings.ToListAsync();
        }

        // POST: api/Buyings
        [HttpPost]
        public async Task<ActionResult<Buying>> PostBuying(Buying buying)
        {
            var schedule = _context.Schedules.FirstOrDefault(x => x.ID == buying.ScheduleId);
            if (schedule == null)
                return NotFound();
            buying.HistorySchedule = $"{schedule.From.Name} - {schedule.To.Name} / {schedule.Time} / {schedule.DayOfWeek.ToString()} / {schedule.Price}";
            _context.Buyings.Add(buying);
            await _context.SaveChangesAsync();

            return buying;
        }
    }
}
