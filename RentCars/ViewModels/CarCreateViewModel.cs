using RentCars.Commons.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentCars.ViewModels
{
    /// <summary>
    /// ViewModel for creating a new car
    /// </summary>
    public class CarCreateViewModel
    {
        /// <summary>
        /// Brand of the car
        /// </summary>
        [Required(ErrorMessage = "The Brand field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Brand must be between {2} and {1} characters.")]
        public string Brand { get; set; }

        /// <summary>
        /// Model of the car
        /// </summary>
        [Required(ErrorMessage = "The Model field is required.")]
        public string Model { get; set; }

        /// <summary>
        /// Engine type of the car (enum)
        /// </summary>
        [Required(ErrorMessage = "The Engine Type field is required.")]
        public EngineType EngineType { get; set; }

        /// <summary>
        /// Image of the car
        /// </summary>
        [Required(ErrorMessage = "Please select an image.")]
        public IFormFile Image { get; set; }

        /// <summary>
        /// Year of the car
        /// </summary>
        [Required(ErrorMessage = "The Year field is required.")]
        [Range(1900, 2024, ErrorMessage = "The Year must be between {1} and {2}.")]
        public int Year { get; set; }

        /// <summary>
        /// Passenger capacity of the car
        /// </summary>
        [Required(ErrorMessage = "The Passenger Capacity field is required.")]
        [Range(1, 16, ErrorMessage = "The Passenger Capacity must be between {1} and {2}.")]
        public int PassengerCapacity { get; set; }

        /// <summary>
        /// Description of the car
        /// </summary>
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        /// <summary>
        /// Rental price per day of the car
        /// </summary>
        [Required(ErrorMessage = "The Rental Price Per Day field is required.")]
        [Range(10, 10000, ErrorMessage = "The Rental Price Per Day must be at least {1}.")]
        public decimal RentalPricePerDay { get; set; }
    }
}
