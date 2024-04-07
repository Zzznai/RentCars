using RentCars.Models;

namespace RentCars.ViewModels
{
    public class SearchViewModel
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        public List<Car> Cars { get; set; }
    }
}
