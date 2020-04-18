using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranslateChatter.Data;
using TranslateChatter.Models;
using TranslateChatter.ViewModels;

namespace TranslateChatter.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> userManager;
        private async Task<User> CurrentUser() => await userManager.FindByEmailAsync(User.Identity.Name);

        public HomeController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rooms.ToListAsync());
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Room room)
        {
            if (ModelState.IsValid)
            {
                var userId = (await CurrentUser()).Id;
                var roomCounts = _context.Rooms.Count(x => x.CreatorId == userId);
                if (roomCounts > 2)
                    return RedirectToAction("Index");
                room.CreatorId = userId;
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Room room)
        {
            if (id != room.Id)
                return NotFound();

            var currentUser = await CurrentUser();
            var currentRoom = _context.Rooms.FirstOrDefault(x => x.Id == id);
            if (currentRoom == null || currentUser.Id != currentRoom?.CreatorId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                currentRoom.Name = room.Name;
                _context.Update(currentRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var currentUser = await CurrentUser();
            var room = await _context.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            if (room == null || room.CreatorId != currentUser.Id)
                return NotFound();
            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> RoomChat(int? id)
        {
            if (!id.HasValue)
                return NotFound();
            var findRoom = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (findRoom == null)
                return NotFound();
            var user = await CurrentUser();
            return View(new RoomViewModel { Room = findRoom, IsAction = findRoom?.CreatorId == user.Id });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUser = await CurrentUser();
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (room.CreatorId != currentUser.Id)
                return NotFound();
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
