using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Models
{
    public class RentCarUser : IdentityUser
    {
        [Required(ErrorMessage = "The First Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The First Name must be between {2} and {1} characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Last Name must be between {2} and {1} characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Unique Citizenship Number field is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The Unique Citizenship Number must be a 10-digit number.")]
        public string UniqueCitinzenshipNumber { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
