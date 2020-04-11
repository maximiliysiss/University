using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Models;
using PeopleAnalysis.ViewModels;

namespace PeopleAnalysis.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;

        public UsersController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        // GET: LoginViewModels
        public async Task<IActionResult> Index()
        {
            List<UserViewModel> userModels = new List<UserViewModel>();
            foreach (var user in userManager.Users.ToList())
            {
                var role = await userManager.GetRolesAsync(user);
                userModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Login = user.Email,
                    Role = role.FirstOrDefault()
                });
            }
            return View(userModels);
        }

        // GET: LoginViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password")] UserViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if ((await userManager.FindByEmailAsync(loginViewModel.Login)) != null)
                    return BadRequest();
                await userManager.CreateAsync(new Models.User { Email = loginViewModel.Login, UserName = loginViewModel.Login }, loginViewModel.Password);
                return RedirectToAction(nameof(Index));
            }
            return View(loginViewModel);
        }

        // GET: LoginViewModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var loginViewModel = await userManager.FindByIdAsync(id);
            if (loginViewModel == null)
                return NotFound();
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Login,Password")] UserViewModel loginViewModel)
        {
            if (id != loginViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(id);
                user.Email = loginViewModel.Login;
                await userManager.UpdateAsync(user);
                await userManager.ChangePasswordAsync(user, loginViewModel.CurrentPassword, loginViewModel.Password);
                return RedirectToAction(nameof(Index));
            }
            return View(loginViewModel);
        }

        // POST: LoginViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm]string toDelete)
        {
            var loginViewModel = await userManager.FindByIdAsync(toDelete);
            if (loginViewModel == null)
                return NotFound();
            await userManager.DeleteAsync(loginViewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}