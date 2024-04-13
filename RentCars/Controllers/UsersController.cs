using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentCars.Commons;
using RentCars.Models;
using RentCars.ViewModels;

namespace RentCars.Controllers
{
    /// <summary>
    /// Controller responsible for managing user-related actions.
    /// </summary>
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class UsersController : Controller
    {
        private readonly UserManager<RentCarUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager service.</param>
        public UsersController(UserManager<RentCarUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Displays a list of users.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var users = (await this.userManager.GetUsersInRoleAsync(GlobalConstants.UserRoleName)).ToList();
            return View(users);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        [HttpPost]
        [Route("/users/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await userManager.DeleteAsync(user);
            

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Redirects to Create
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            return RedirectToAction("Create");
        }

        /// <summary>
        /// Displays the user creation view.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            UserSignUpViewModel userSignUpViewModel = new UserSignUpViewModel();
            return View(userSignUpViewModel);
        }

        /// <summary>
        /// Handles user creation.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserSignUpViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            if (ModelState.IsValid)
            {
                if (userManager.Users.Any(u => u.UniqueCitinzenshipNumber == userModel.UniqueCitinzenshipNumber))
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
                    await userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(userModel);
        }

        /// <summary>
        /// Displays the user edit view.
        /// </summary>
        [HttpGet]
        [Route("/edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userModel = new UserEditViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UniqueCitinzenshipNumber = user.UniqueCitinzenshipNumber,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

            return View(userModel);
        }

        /// <summary>
        /// Handles user editing.
        /// </summary>
        [HttpPost]
        [Route("/edit/{id}")]
        public async Task<IActionResult> Edit(string id, UserEditViewModel userModel)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(userModel);
            }

            if (userManager.Users.Any(u => u.UniqueCitinzenshipNumber == userModel.UniqueCitinzenshipNumber && u.Id != userModel.Id))
            {
                ModelState.AddModelError(string.Empty, "A user with the same EGN already exists.");
                return View(userModel);
            }

            if (userManager.Users.Any(u => u.Email == userModel.Email && u.Id != userModel.Id))
            {
                ModelState.AddModelError(string.Empty, "A user with the same email already exists.");
                return View(userModel);
            }

            user.UserName = userModel.Username;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.PhoneNumber = userModel.PhoneNumber;
            user.Email = userModel.Email;
            user.UniqueCitinzenshipNumber = userModel.UniqueCitinzenshipNumber;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                if (userModel.Password != null)
                {
                    await userManager.RemovePasswordAsync(user);
                    var passwordCheck = await userManager.AddPasswordAsync(user, userModel.Password);

                    if (!passwordCheck.Succeeded)
                    {
                        foreach (var error in passwordCheck.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                            return View(userModel);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(userModel);
        }
    }
}
