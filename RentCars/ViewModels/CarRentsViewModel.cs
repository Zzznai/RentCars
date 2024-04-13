using RentCars.Models;
using System.Collections.Generic;

namespace RentCars.ViewModels
{
    /// <summary>
    /// ViewModel for displaying reservations associated with a car
    /// </summary>
    public class CarRentsViewModel
    {
        /// <summary>
        /// Brand of the car
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Model of the car
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Year of the car
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// List of reservations associated with the car
        /// </summary>
        public List<Reservation> Reservations { get; set; }
    }
}
