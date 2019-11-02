using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoStation.Models;
using AutoStation.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AutoStation.Controllers
{
    /// <summary>
    /// Интеграция
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IntegrationController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        public DatabaseContext context;

        public IntegrationController(DatabaseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Загрузить Excel документ
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<List<Schedule>>> UploadSchedule([FromForm]IFormFile file)
        {
            if (file.Length <= 0)
                return NotFound();

            context.Schedules.RemoveRange(context.Schedules);
            context.Points.RemoveRange(context.Points);

            using (var workBook = new XLWorkbook(file.OpenReadStream()))
            {
                foreach (var row in workBook.Worksheet(1).Rows().Skip(1))
                {
                    var fromName = row.Cell(1).Value.ToString().Trim();
                    var toName = row.Cell(2).Value.ToString().Trim();

                    var from = context.Points.FirstOrDefault(x => x.Name.ToLower() == fromName.ToLower());
                    var to = context.Points.FirstOrDefault(x => x.Name.ToLower() == toName.ToLower());
                    if (from == null)
                        from = new Point { Name = fromName };
                    if (to == null)
                        to = new Point { Name = toName };

                    var schedule = new Schedule
                    {
                        DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row.Cell(3).Value.ToString()),
                        Distance = double.Parse(row.Cell(4).Value.ToString()),
                        From = from,
                        Price = double.Parse(row.Cell(5).Value.ToString()),
                        Time = ((DateTime)row.Cell(6).Value).ToShortTimeString(),
                        To = to
                    };

                    context.Add(schedule);
                    await context.SaveChangesAsync();
                }
            }

            return context.Schedules.ToList();
        }
    }
}