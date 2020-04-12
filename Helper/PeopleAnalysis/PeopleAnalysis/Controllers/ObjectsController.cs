using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleAnalysis.Models;
using PeopleAnalysis.Services;

namespace PeopleAnalysis.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ObjectsController : Controller
    {
        private readonly DatabaseContext _context;

        public ObjectsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: AnalysObjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnalysObjects.ToListAsync());
        }

        // GET: AnalysObjects/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnalysObject analysObject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(analysObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(analysObject);
        }

        // GET: AnalysObjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analysObject = await _context.AnalysObjects.FindAsync(id);
            if (analysObject == null)
            {
                return NotFound();
            }
            return View(analysObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnalysObject analysObject)
        {
            if (id != analysObject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(analysObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalysObjectExists(analysObject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(analysObject);
        }

        // POST: AnalysObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var analysObject = await _context.AnalysObjects.FindAsync(id);
            _context.AnalysObjects.Remove(analysObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalysObjectExists(int id) => _context.AnalysObjects.Any(e => e.Id == id);
    }
}