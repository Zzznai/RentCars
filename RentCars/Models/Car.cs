using RentCars.Commons.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Models
{
    /// <summary>
    /// Represents a car
    /// </summary>
    public class Car
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The brand of the car.
        /// </summary>
        [Required(ErrorMessage = "The Brand field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Brand must be between {2} and {1} characters.")]
        public string Brand { get; set; }

        /// <summary>
        /// The model of the car.
        /// </summary>
        [Required(ErrorMessage = "The Model field is required.")]
        public string Model { get; set; }

        /// <summary>
        /// The type of engine the car has. (enum)
        /// </summary>
        [Required(ErrorMessage = "The Engine Type field is required.")]
        public EngineType EngineType { get; set; }

        /// <summary>
        /// The URL to the image of the car.
        /// </summary>
        [Required(ErrorMessage = "The Image Url field is required.")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// The year the car was manufactured.
        /// </summary>
        [Required(ErrorMessage = "The Year field is required.")]
        [Range(1900, 2024, ErrorMessage = "The Year must be between {1} and {2}.")]
        public int Year { get; set; }

        /// <summary>
        /// The passenger capacity of the car.
        /// </summary>
        [Required(ErrorMessage = "The Passenger Capacity field is required.")]
        [Range(1, 16, ErrorMessage = "The Passenger Capacity must be between {1} and {2}.")]
        public int PassengerCapacity { get; set; }

        /// <summary>
        /// A brief description of the car.
        /// </summary>
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        /// <summary>
        /// The rental price per day for the car.
        /// </summary>
        [Required(ErrorMessage = "The Rental Price Per Day field is required.")]
        [Range(10, 10000, ErrorMessage = "The Rental Price Per Day must be between {1} and {2}.")]
        public decimal RentalPricePerDay { get; set; }

        /// <summary>
        /// The list of reservations made for this car.
        /// </summary>
        public List<Reservation> Reservations { get; set; }
    }
}
