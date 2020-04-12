using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Extensions;
using PeopleAnalysis.Models;
using PeopleAnalysis.ViewModels;

namespace PeopleAnalysis.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Create(UserViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if ((await userManager.FindByEmailAsync(loginViewModel.Login)) != null)
                    return this.Error(loginViewModel, "Такой пользователь уже существует");
                var newUser = new User { Email = loginViewModel.Login, UserName = loginViewModel.Login };
                await userManager.CreateAsync(newUser, loginViewModel.Password);
                await userManager.AddToRoleAsync(newUser, "User");
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
            return View(new UserViewModel
            {
                Id = loginViewModel.Id,
                Login = loginViewModel.Email,
                Role = (await userManager.GetRolesAsync(loginViewModel)).FirstOrDefault()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel loginViewModel)
        {
            if (id != loginViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(id);
                user.Email = loginViewModel.Login;
                var res = await userManager.UpdateAsync(user);
                if (!res.Succeeded)
                    return this.Error(loginViewModel, string.Join(", ", res.Errors.Select(x => x.Description)));
                res = await userManager.ChangePasswordAsync(user, loginViewModel.CurrentPassword, loginViewModel.Password);
                if (!res.Succeeded)
                    return this.Error(loginViewModel, string.Join(", ", res.Errors.Select(x => x.Description)));
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