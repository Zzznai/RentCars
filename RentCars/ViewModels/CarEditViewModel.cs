using RentCars.Commons.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentCars.ViewModels
{
    public class CarEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public EngineType EngineType { get; set; }

        public IFormFile? Image { get; set; }

        [Required]
        [Range(1900, 2024)]
        public int Year { get; set; }

        [Required]
        [Range(1, 16)]
        public int PassengerCapacity { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal RentalPricePerDay { get; set; }
    }
}
