namespace RentCars.ViewModels
{
    /// <summary>
    /// ViewModel for creating a reservation
    /// </summary>
    public class ReservationCreateViewModel
    {
        /// <summary>
        /// Start date of the reservation
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the reservation
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
