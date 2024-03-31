using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Models
{
    public class RentCarUser : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string UniqueCitinzenshipNumber { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
