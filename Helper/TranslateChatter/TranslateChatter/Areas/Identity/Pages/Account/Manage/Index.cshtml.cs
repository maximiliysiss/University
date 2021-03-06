﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TranslateChatter.AuthAPI;
using TranslateChatter.Extensions;
using TranslateChatter.Models;
using TranslateChatter.Services;

namespace TranslateChatter.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IAuthAPIClient authAPIClient;
        private readonly ITokenService tokenService;

        public IndexModel(IAuthAPIClient authAPIClient, ITokenService tokenService)
        {
            this.authAPIClient = authAPIClient;
            this.tokenService = tokenService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Language")]
            public string Language { get; set; }

        }

        private async Task LoadAsync()
        {
            var currentUser = await authAPIClient.ApiUserAsync(tokenService.GenerateFullToken(User.Token()));

            Username = currentUser.Nickname;

            Input = new InputModel
            {
                Language = currentUser.Language.Code
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await authAPIClient.ApiUserAsync(User.Token());
            if (user == null)
                return NotFound($"Unable to load user with ID '{User.Id()}'.");
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await authAPIClient.ApiUserAsync(User.Token());
            if (user == null)
                return NotFound($"Unable to load user with ID '{User.Id()}'.");

            if (!ModelState.IsValid)
            {
                await LoadAsync();
                return Page();
            }

            if (Input.Language != user.Language.Name)
                await authAPIClient.ApiUserChangelanguageAsync(User.Token(), new ChangeLanguageModel { Language = Input.Language });
            var refresh = await authAPIClient.ApiAuthRefreshtokenAsync(User.Refresh(), User.Token());
            await tokenService.SignInAsync(refresh);

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
