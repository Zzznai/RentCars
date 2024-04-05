using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentCars.Models;
using RentCars.ViewModels;

namespace RentCars.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<RentCarUser> signInManager;

        public AuthenticationController(SignInManager<RentCarUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("sign-in")]
        public IActionResult SignIn(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            return User.Identity.IsAuthenticated ? this.LocalRedirect(returnUrl) : this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn(RentCarUserViewModel userModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (User.Identity.IsAuthenticated)
            {
                return this.LocalRedirect(returnUrl);
            }

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(userModel.Username, userModel.Password, true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return this.LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password!");
                }
            }
           

            return this.View(userModel);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
