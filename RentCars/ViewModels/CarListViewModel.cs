namespace RentCars.ViewModels
{
    public class CarListViewModel
    {
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public int Year { get; set; }
        public string EngineType { get; set; }
        public int PassengerCapacity { get; set; }
        public string? Description { get; set; }
    }
}
