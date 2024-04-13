namespace RentCars.ViewModels
{
    /// <summary>
    /// ViewModel for displaying a list of cars
    /// </summary>
    public class CarListViewModel
    {
        /// <summary>
        /// URL of the car image
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Brand of the car
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Model of the car
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Rental price per day of the car
        /// </summary>
        public decimal RentalPricePerDay { get; set; }

        /// <summary>
        /// Year of the car
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Engine type of the car
        /// </summary>
        public string EngineType { get; set; }

        /// <summary>
        /// Passenger capacity of the car
        /// </summary>
        public int PassengerCapacity { get; set; }

        /// <summary>
        /// Description of the car
        /// </summary>
        public string? Description { get; set; }
    }
}
