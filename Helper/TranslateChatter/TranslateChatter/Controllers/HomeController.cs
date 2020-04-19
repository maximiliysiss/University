using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranslateChatter.ChatAPI;
using TranslateChatter.Extensions;
using TranslateChatter.ViewModels;

namespace TranslateChatter.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IChatAPIClient chatAPIClient;

        public HomeController(IChatAPIClient chatAPIClient)
        {
            this.chatAPIClient = chatAPIClient;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await chatAPIClient.ApiRoomsGetAsync());
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ChatAPI.Room room)
        {
            if (ModelState.IsValid)
            {
                var roomCounts = await chatAPIClient.ApiRoomsUserAsync(User.Id());
                if (roomCounts.Count > 2)
                    return RedirectToAction("Index");
                room.CreatorId = User.Id();
                room = await chatAPIClient.ApiRoomsPostAsync(room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var room = await chatAPIClient.ApiRoomsGetAsync(id.Value);
            if (room == null)
                return NotFound();
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ChatAPI.Room room)
        {
            if (id != room.Id)
                return NotFound();

            var currentRoom = await chatAPIClient.ApiRoomsGetAsync(room.Id);
            if (currentRoom == null || User.Id() != currentRoom?.CreatorId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                currentRoom.Name = room.Name;
                await chatAPIClient.ApiRoomsPutAsync(id, currentRoom);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var room = await chatAPIClient.ApiRoomsGetAsync(id.Value);
            if (room == null || room.CreatorId != User.Id())
                return NotFound();
            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> RoomChat(int? id)
        {
            if (!id.HasValue)
                return NotFound();
            var findRoom = await chatAPIClient.ApiRoomsGetAsync(id.Value);
            if (findRoom == null)
                return NotFound();
            return View(new RoomViewModel { Room = findRoom, IsAction = findRoom?.CreatorId == User.Id() });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await chatAPIClient.ApiRoomsGetAsync(id);
            if (room.CreatorId != User.Id())
                return NotFound();
            await chatAPIClient.ApiRoomsDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
