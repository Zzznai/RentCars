using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Models
{
    /// <summary>
    /// Represents a user of the RentCars application.
    /// Inherits from <see cref="IdentityUser"/>, the base class for user objects managed by ASP.NET Core Identity.
    /// </summary>
    public class RentCarUser : IdentityUser
    {
        /// <summary>
        /// The first name of the user.
        /// </summary>
        [Required(ErrorMessage = "The First Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The First Name must be between {2} and {1} characters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        [Required(ErrorMessage = "The Last Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Last Name must be between {2} and {1} characters.")]
        public string LastName { get; set; }

        /// <summary>
        /// The unique citizenship number of the user.
        /// </summary>
        [Required(ErrorMessage = "The Unique Citizenship Number field is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The Unique Citizenship Number must be a 10-digit number.")]
        public string UniqueCitinzenshipNumber { get; set; }

        /// <summary>
        /// The list of reservations made by the user.
        /// </summary>
        public List<Reservation> Reservations { get; set; }
    }
}
