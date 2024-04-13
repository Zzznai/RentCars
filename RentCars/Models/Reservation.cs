using RentCars.Commons.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Models
{
    /// <summary>
    /// Represents a reservation made by a user for a car.
    /// </summary>
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The start date of the reservation.
        /// </summary>
        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the reservation.
        /// </summary>
        [Required(ErrorMessage = "The End Date field is required.")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The total rental amount for the reservation.
        /// </summary>
        public decimal RentalSum { get; set; }

        /// <summary>
        /// The status of the reservation.
        /// </summary>
        public ReservationStatus Status { get; set; }

        /// <summary>
        /// The user who made the reservation.
        /// </summary>
        [Required(ErrorMessage = "The User field is required.")]
        public RentCarUser User { get; set; }

        /// <summary>
        /// The car reserved by the user.
        /// </summary>
        [Required(ErrorMessage = "The Car field is required.")]
        public Car Car { get; set; }
    }
}
