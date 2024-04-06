using System.ComponentModel.DataAnnotations;

namespace RentCars.ViewModels
{
    public class UserSignInViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
