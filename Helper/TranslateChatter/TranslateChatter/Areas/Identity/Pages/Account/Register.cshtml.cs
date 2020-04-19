using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TranslateChatter.AuthAPI;
using TranslateChatter.Models;
using TranslateChatter.Services;

namespace TranslateChatter.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IAuthAPIClient authAPIClient;
        private readonly ITokenService tokenService;

        public RegisterModel(IAuthAPIClient authAPIClient, ITokenService tokenService)
        {
            this.authAPIClient = authAPIClient;
            this.tokenService = tokenService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await authAPIClient.ApiAuthRegisterAsync(new AuthAPI.RegisterModel { Email = Input.Email, Nickname = Input.Email, Password = Input.Password });
                    if (result != null)
                    {
                        await tokenService.SignInAsync(result.AccessToken);
                        return LocalRedirect(returnUrl);
                    }
                }
                catch (ApiException)
                {
                    ModelState.AddModelError(string.Empty, "Invalid register attempt.");
                }
            }

            return Page();
        }
    }
}
