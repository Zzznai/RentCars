using System.ComponentModel.DataAnnotations;

namespace RentCars.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public RentCarUser User { get; set; }

        [Required]
        public Car Car { get; set; }

    }
}
