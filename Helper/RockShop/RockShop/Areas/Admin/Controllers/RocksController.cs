using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockShop.Areas.Admin.ViewModels.Rocks;
using RockShop.Data.Models;
using RockShop.Extensions;
using RockShop.Services;
using System;
using System.Threading.Tasks;

namespace RockShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RocksController : Controller
    {
        private readonly IRockService rockService;

        public RocksController(IRockService rockService)
        {
            this.rockService = rockService;
        }

        // GET: Rocks
        public async Task<IActionResult> Index()
        {
            return View(await rockService.GetRocksAsync());
        }

        // GET: Rocks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rock = await rockService.GetRockByIdAsync(id.Value);
            if (rock == null)
            {
                return NotFound();
            }

            return View(rock);
        }

        // GET: Rocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateRockRequest rock)
        {
            if (ModelState.IsValid)
            {
                await rockService.CreateRockAsync(new Rock
                {
                    Description = rock.Description,
                    Name = rock.Name,
                    Price = rock.Price,
                    Image = Convert.ToBase64String(await rock.Image.OpenReadStream().ToByteArrayAsync())
                });
                return RedirectToAction(nameof(Index));
            }

            return View(new Rock
            {
                Description = rock.Description,
                Name = rock.Name,
                Price = rock.Price
            });
        }

        // GET: Rocks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rock = await rockService.GetRockByIdAsync(id.Value);
            if (rock == null)
            {
                return NotFound();
            }

            return View(rock);
        }

        // POST: Rocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [FromForm] EditRockRequest rock)
        {
            if (id != rock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string image = null;
                    if (rock.Image != null)
                        image = Convert.ToBase64String(await rock.Image.OpenReadStream().ToByteArrayAsync());

                    await rockService.UpdateRockAsync(new Rock
                    {
                        Id = id,
                        Description = rock.Description,
                        Name = rock.Name,
                        Price = rock.Price,
                        Image = image,
                        CreationDateTime = rock.CreationDateTime
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await rockService.RockExistsAsync(rock.Id))
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

            return View(new Rock
            {
                Id = id,
                Description = rock.Description,
                Name = rock.Name,
                Price = rock.Price,
                Image = rock.CurrentImage,
                CreationDateTime = rock.CreationDateTime
            });
        }

        // GET: Rocks/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rock = await rockService.GetRockByIdAsync(id.Value);
            if (rock == null)
            {
                return NotFound();
            }

            return View(rock);
        }

        // POST: Rocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await rockService.DeleteRockAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
