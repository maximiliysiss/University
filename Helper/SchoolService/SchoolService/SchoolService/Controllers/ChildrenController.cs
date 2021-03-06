﻿using System;
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
    [ApiController]
    [Authorize]
    public class ChildrenController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ChildrenController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Children
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Child>>> GetChildren()
        {
            return await _context.Children.Where(x => !x.IsArchive).ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<Teacher> GetChildren(int id) => RedirectToAction("GetUser", "Users", new { id = id });

        [HttpGet("archived")]
        [Authorize(Roles = "Admin, KnowledgeTeacher, Social")]
        public async Task<ActionResult<IEnumerable<Child>>> GetArchived()
        {
            return await _context.Children.Where(x => x.IsArchive).ToListAsync();
        }

        // PUT: api/Children/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, KnowledgeTeacher")]
        public async Task<ActionResult<Child>> PutChild(int id, Child child)
        {
            if (id != child.ID)
                return BadRequest();

            child.PasswordHash = CryptService.CreateMD5(child.PasswordHash);
            _context.Entry(child).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildExists(id))
                    return NotFound();
                else
                    throw;
            }

            return child;
        }

        [HttpGet("class/{id}")]
        public ActionResult<List<Child>> GetChildByClass(int id) => _context.Children.Where(x => x.Class.ID == id).ToList();

        // GET: api/3/class/3
        [HttpGet("{id}/class/{class}")]
        [Authorize(Roles = "Admin, KnowledgeTeacher")]
        public async Task<ActionResult<Child>> ChangeClass(int id, int @class)
        {
            var child = _context.Children.FirstOrDefault(x => x.ID == id);
            var cl = _context.Classes.FirstOrDefault(x => x.ID == @class);

            if (child == null || cl == null)
                return NotFound();

            child.Class = cl;
            _context.Entry(child).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return child;
        }

        // POST: api/Children
        [HttpPost]
        [Authorize(Roles = "Admin, KnowledgeTeacher")]
        public async Task<ActionResult<Child>> PostChild(Child child)
        {
            if (_context.Children.Any(x => x.Login == child.Login))
                return BadRequest();

            child.PasswordHash = CryptService.CreateMD5(child.PasswordHash);
            _context.Children.Add(child);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", "Users", new { id = child.ID }, child);
        }

        private bool ChildExists(int id) => _context.Children.Any(e => e.ID == id);

        [HttpGet("{id}/archive")]
        [Authorize(Roles = "Admin, KnowledgeTeacher, Social")]
        public ActionResult<Child> Archive(int id)
        {
            var child = _context.Children.FirstOrDefault(x => x.ID == id);
            if (child == null)
                return NotFound();
            child.IsArchive = !child.IsArchive;
            _context.Update(child);
            _context.SaveChanges();
            return child;
        }
    }
}
