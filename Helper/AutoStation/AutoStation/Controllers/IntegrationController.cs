using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoStation.Models;
using AutoStation.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoStation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        public DatabaseContext context;

        public IntegrationController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<Schedule>>> UploadSchedule([FromBody]IFormFile file)
        {
            context.Schedules.RemoveRange(context.Schedules);
            context.Points.RemoveRange(context.Points);

            using (var workBook = new XLWorkbook(file.OpenReadStream()))
            {
                foreach (var row in workBook.Worksheet(0).Rows().Skip(1))
                {
                    var fromName = row.Cell(0).ToString().Trim();
                    var toName = row.Cell(1).ToString().Trim();

                    var from = context.Points.FirstOrDefault(x => x.Name.ToLower() == fromName.ToLower());
                    var to = context.Points.FirstOrDefault(x => x.Name.ToLower() == toName.ToLower());
                    if (from == null)
                        from = new Point { Name = fromName };
                    if (to == null)
                        to = new Point { Name = toName };

                    var schedule = new Schedule
                    {
                        DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row.Cell(2).Value.ToString()),
                        Distance = double.Parse(row.Cell(3).ToString()),
                        From = from,
                        Price = double.Parse(row.Cell(4).ToString()),
                        Time = row.Cell(5).ToString(),
                        To = to
                    };

                    context.Add(schedule);
                }
            }

            await context.SaveChangesAsync();
            return context.Schedules.ToList();
        }
    }
}