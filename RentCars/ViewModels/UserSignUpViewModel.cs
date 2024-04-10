using System.ComponentModel.DataAnnotations;

namespace RentCars.ViewModels
{
    public class UserSignUpViewModel
    {
        [Required(ErrorMessage = "The Username field is required.")]
        [MinLength(3, ErrorMessage = "The Username must be at least {1} characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The First Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The First Name must be between {2} and {1} characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Last Name must be between {2} and {1} characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Unique Citizenship Number field is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The Unique Citizenship Number must be a 10-digit number.")]
        public string UniqueCitinzenshipNumber { get; set; }

        [Required(ErrorMessage = "The Phone Number field is required.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [Compare("ConfirmPassword", ErrorMessage = "Password must match the Confirm Password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Confirm Password field is required.")]
        [Compare("Password", ErrorMessage = "The Confirm Password must match the Password.")]
        public string ConfirmPassword { get; set; }
    }
}
