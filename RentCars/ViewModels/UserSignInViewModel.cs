using System.ComponentModel.DataAnnotations;

namespace RentCars.ViewModels
{
    /// <summary>
    /// ViewModel for user sign-in
    /// </summary>
    public class UserSignInViewModel
    {
        /// <summary>
        /// The username of the user
        /// </summary>
        [Required(ErrorMessage = "The Username field is required.")]
        public string Username { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }
    }
}
