using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentCars.Commons;
using RentCars.Data;
using RentCars.Models;
using RentCars.ViewModels;
using System.Threading.Tasks;

namespace RentCars.Controllers
{
    /// <summary>
    /// Controller responsible for managing user profiles.
    /// </summary>
    [Authorize(Roles = GlobalConstants.UserRoleName)]
    public class ProfileController : Controller
    {
        private readonly UserManager<RentCarUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="userManager">The UserManager for managing user operations.</param>
        public ProfileController(UserManager<RentCarUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Displays the user's profile information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(User);
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userProfileModel = new UserEditViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UniqueCitinzenshipNumber = user.UniqueCitinzenshipNumber,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

            return View(userProfileModel);
        }

        /// <summary>
        /// Handles updating the user's profile information.
        /// </summary>
        /// <param name="userProfileModel">The updated user profile data.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserEditViewModel userProfileModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userProfileModel);
            }

            var userId = userManager.GetUserId(User);
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.UserName = userProfileModel.Username;
            user.FirstName = userProfileModel.FirstName;
            user.LastName = userProfileModel.LastName;
            user.PhoneNumber = userProfileModel.PhoneNumber;
            user.Email = userProfileModel.Email;
            user.UniqueCitinzenshipNumber = userProfileModel.UniqueCitinzenshipNumber;

            // Update user in the database
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(userProfileModel.Password))
                {
                    // If password change is requested, update the password
                    var passwordChangeResult = await userManager.RemovePasswordAsync(user);
                    if (passwordChangeResult.Succeeded)
                    {
                        passwordChangeResult = await userManager.AddPasswordAsync(user, userProfileModel.Password);
                        if (!passwordChangeResult.Succeeded)
                        {
                            // Handle password change failure
                            foreach (var error in passwordChangeResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(userProfileModel);
                        }
                    }
                    else
                    {
                        // Handle password change failure
                        foreach (var error in passwordChangeResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(userProfileModel);
                    }
                }

                // Redirect to the home page after successful update
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // If update fails, add errors to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Return the view with updated user profile model
            return View(userProfileModel);
        }
    }
}
