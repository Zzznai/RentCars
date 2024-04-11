using RentCars.Models;

namespace RentCars.ViewModels
{
    public class UserRentsViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
