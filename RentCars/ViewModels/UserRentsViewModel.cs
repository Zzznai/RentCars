using RentCars.Models;
using System.Collections.Generic;

namespace RentCars.ViewModels
{
    /// <summary>
    /// ViewModel for displaying a user's reservations
    /// </summary>
    public class UserRentsViewModel
    {
        /// <summary>
        /// The unique identifier of the user
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The username of the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The first name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The list of reservations made by the user
        /// </summary>
        public List<Reservation> Reservations { get; set; }
    }
}
