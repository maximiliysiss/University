using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolService.Models;
using SchoolService.Services;

namespace SchoolService.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeAttribute(Roles = "Admin, Social")]
    [ApiController]
    public class ChildInRiskGroupsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ChildInRiskGroupsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ChildInRiskGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChildInRiskGroup>>> GetChildInRiskGroups()
        {
            return await _context.ChildInRiskGroups.ToListAsync();
        }

        // GET: api/ChildInRiskGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChildInRiskGroup>> GetChildInRiskGroup(int id)
        {
            var childInRiskGroup = await _context.ChildInRiskGroups.FindAsync(id);

            if (childInRiskGroup == null)
            {
                return NotFound();
            }

            return childInRiskGroup;
        }

        // POST: api/ChildInRiskGroups
        [HttpPost]
        public async Task<ActionResult<ChildInRiskGroup>> PostChildInRiskGroup(ChildInRiskGroup childInRiskGroup)
        {
            _context.ChildInRiskGroups.Add(childInRiskGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChildInRiskGroup", new { id = childInRiskGroup.ID }, childInRiskGroup);
        }

        // DELETE: api/ChildInRiskGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ChildInRiskGroup>> DeleteChildInRiskGroup(int id)
        {
            var childInRiskGroup = await _context.ChildInRiskGroups.FindAsync(id);
            if (childInRiskGroup == null)
            {
                return NotFound();
            }

            _context.ChildInRiskGroups.Remove(childInRiskGroup);
            await _context.SaveChangesAsync();

            return childInRiskGroup;
        }
    }
}
