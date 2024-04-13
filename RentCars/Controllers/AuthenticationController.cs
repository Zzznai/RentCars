using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentCars.Commons;
using RentCars.Models;
using RentCars.ViewModels;

namespace RentCars.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<RentCarUser> signInManager;
        private readonly UserManager<RentCarUser> userManager;

        public AuthenticationController(SignInManager<RentCarUser> signInManager, UserManager<RentCarUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
        public async Task<IActionResult> SignIn(UserSignInViewModel userModel, string returnUrl = null)
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

        [HttpGet]
        [AllowAnonymous]
        [Route("sign-up")]
        public IActionResult Signup(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            return User.Identity.IsAuthenticated ? this.LocalRedirect(returnUrl) : this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp(UserSignUpViewModel userModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl);
            }

            if (ModelState.IsValid)
            {
                if (userManager.Users.Any(u=>u.UniqueCitinzenshipNumber==userModel.UniqueCitinzenshipNumber))
                {
                    ModelState.AddModelError(string.Empty, "A user with the same EGN already exists.");
                    return View(userModel);
                }

                if (userManager.Users.Any(u => u.Email == userModel.Email))
                {
                    ModelState.AddModelError(string.Empty, "A user with the same email already exists.");
                    return View(userModel);
                }

                var user = new RentCarUser()
                {
                    UserName = userModel.Username,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    UniqueCitinzenshipNumber = userModel.UniqueCitinzenshipNumber,
                    PhoneNumber = userModel.PhoneNumber,
                    Email = userModel.Email
                };

                var result = await userManager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,GlobalConstants.UserRoleName);
                    return RedirectToAction("SignIn", "Authentication");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(userModel);
                }
            }

            return View(userModel);
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
