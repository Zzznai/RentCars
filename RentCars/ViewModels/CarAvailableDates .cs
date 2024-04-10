using RentCars.Models;
using System;

namespace RentCars.ViewModels
{
    public class CarAvailableDates
    {
        public string Brand { get; set; }
        public string Model { get; set; } 
        public int Year { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}