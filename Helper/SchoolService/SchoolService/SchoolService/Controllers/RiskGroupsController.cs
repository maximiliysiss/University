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
    public class RiskGroupsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RiskGroupsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/RiskGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiskGroup>>> GetRiskGroups()
        {
            return await _context.RiskGroups.ToListAsync();
        }

        // GET: api/RiskGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RiskGroup>> GetRiskGroup(int id)
        {
            var riskGroup = await _context.RiskGroups.FindAsync(id);

            if (riskGroup == null)
            {
                return NotFound();
            }

            return riskGroup;
        }

        // PUT: api/RiskGroups/5
        [HttpPut("{id}")]
        public async Task<ActionResult<RiskGroup>> PutRiskGroup(int id, RiskGroup riskGroup)
        {
            if (id != riskGroup.ID)
            {
                return BadRequest();
            }

            _context.Entry(riskGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiskGroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return riskGroup;
        }

        // POST: api/RiskGroups
        [HttpPost]
        public async Task<ActionResult<RiskGroup>> PostRiskGroup(RiskGroup riskGroup)
        {
            _context.RiskGroups.Add(riskGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRiskGroup", new { id = riskGroup.ID }, riskGroup);
        }

        // DELETE: api/RiskGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RiskGroup>> DeleteRiskGroup(int id)
        {
            var riskGroup = await _context.RiskGroups.FindAsync(id);
            if (riskGroup == null)
            {
                return NotFound();
            }

            _context.RiskGroups.Remove(riskGroup);
            await _context.SaveChangesAsync();

            return riskGroup;
        }

        private bool RiskGroupExists(int id)
        {
            return _context.RiskGroups.Any(e => e.ID == id);
        }

        [HttpGet("risk/{child}/{group}")]
        public async Task<ActionResult<ChildInRiskGroup>> ChangeChildInGroup(int child, int group)
        {
            var _child = _context.Children.FirstOrDefault(x => x.ID == child);
            var riskGroup = _context.RiskGroups.FirstOrDefault(x => x.ID == group);

            if (_child == null || riskGroup == null)
                return NotFound();

            var childInGroup = _context.ChildInRiskGroups.FirstOrDefault(x => x.ChildId == child && x.RiskGroupId == group);
            if (childInGroup == null)
                _context.Add(childInGroup = new ChildInRiskGroup { Child = _child, RiskGroup = riskGroup });
            else
                _context.Remove(childInGroup);
            await _context.SaveChangesAsync();
            return childInGroup;
        }
    }
}
