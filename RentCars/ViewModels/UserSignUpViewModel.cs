using System.ComponentModel.DataAnnotations;

namespace RentCars.ViewModels
{
    public class UserSignUpViewModel
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string UniqueCitinzenshipNumber { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
