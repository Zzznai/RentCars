using RentCars.Models;

namespace RentCars.ViewModels
{
    public class CarRentsViewModel
    {
        public string Brand { get; set; }

        public string Model { get; set;}

        public int Year {  get; set; }

        public List<Reservation> Reservations { get; set; }

    }
}
