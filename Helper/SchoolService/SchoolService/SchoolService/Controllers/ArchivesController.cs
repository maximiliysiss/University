using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolService.Models;
using SchoolService.Services;

namespace SchoolService.Controllers
{
    [Route("api/[controller]")]
    [Authorize("Admin")]
    [ApiController]
    public class ArchivesController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public ArchivesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        public List<Child> ArchiveList() => databaseContext.Children.Where(x => x.IsArchive).ToList();

        [HttpGet("{id}")]
        public ActionResult<Child> AddToArchive(int id)
        {
            var child = databaseContext.Children.FirstOrDefault(x => x.ID == id);
            if (child == null)
                return NotFound();
            child.IsArchive = true;
            databaseContext.Update(child);
            databaseContext.SaveChanges();
            return child;
        }

        [HttpGet("{id}/delete")]
        public ActionResult<Child> DeleteFromArchive(int id)
        {
            var child = databaseContext.Children.FirstOrDefault(x => x.ID == id);
            if (child == null)
                return NotFound();
            child.IsArchive = false;
            databaseContext.Update(child);
            databaseContext.SaveChanges();
            return child;
        }
    }
}