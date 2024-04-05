using System.ComponentModel.DataAnnotations;

namespace RentCars.ViewModels
{
    public class RentCarUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
