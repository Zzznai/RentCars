using RentCars.Commons.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RentCars.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The End Date field is required.")]
        public DateTime EndDate { get; set; }

        public decimal RentalSum { get; set; }

        public ReservationStatus Status { get; set; }

        [Required(ErrorMessage = "The User field is required.")]
        public RentCarUser User { get; set; }

        [Required(ErrorMessage = "The Car field is required.")]
        public Car Car { get; set; }
    }
}
