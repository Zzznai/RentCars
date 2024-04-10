﻿using RentCars.Commons.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Brand field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Brand must be between {2} and {1} characters.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "The Model field is required.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "The Engine Type field is required.")]
        public EngineType EngineType { get; set; }

        [Required(ErrorMessage = "The Image Url field is required.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "The Year field is required.")]
        [Range(1900, 2024, ErrorMessage = "The Year must be between {1} and {2}.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "The Passenger Capacity field is required.")]
        [Range(1, 16, ErrorMessage = "The Passenger Capacity must be between {1} and {2}.")]
        public int PassengerCapacity { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "The Rental Price Per Day field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Rental Price Per Day must be between {1} and {2}.")]
        public decimal RentalPricePerDay { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
