using RentCars.Models;
using System;
using System.Collections.Generic;

namespace RentCars.ViewModels
{
    /// <summary>
    /// ViewModel for search functionality
    /// </summary>
    public class SearchViewModel
    {
        /// <summary>
        /// Start date for the search
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.Now;

        /// <summary>
        /// End date for the search
        /// </summary>
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        /// <summary>
        /// List of cars matching the search criteria
        /// </summary>
        public List<Car> Cars { get; set; }
    }
}
