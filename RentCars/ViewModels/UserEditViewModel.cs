using System.ComponentModel.DataAnnotations;

namespace RentCars.ViewModels
{
    /// <summary>
    /// ViewModel for editing user details
    /// </summary>
    public class UserEditViewModel
    {
        /// <summary>
        /// The unique identifier of the user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The username of the user
        /// </summary>
        [Required(ErrorMessage = "The Username field is required.")]
        [MinLength(3, ErrorMessage = "The Username must be at least {1} characters.")]
        public string Username { get; set; }

        /// <summary>
        /// The first name of the user
        /// </summary>
        [Required(ErrorMessage = "The First Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The First Name must be between {2} and {1} characters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        [Required(ErrorMessage = "The Last Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Last Name must be between {2} and {1} characters.")]
        public string LastName { get; set; }

        /// <summary>
        /// The unique citizenship number of the user
        /// </summary>
        [Required(ErrorMessage = "The Unique Citizenship Number field is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The Unique Citizenship Number must be a 10-digit number.")]
        public string UniqueCitinzenshipNumber { get; set; }

        /// <summary>
        /// The phone number of the user
        /// </summary>
        [Required(ErrorMessage = "The Phone Number field is required.")]
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The email address of the user
        /// </summary>
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
