namespace RentCars.Commons.Enums
{
    /// <summary>
    /// Represents the status of a reservation.
    /// </summary>
    public enum ReservationStatus
    {
        /// <summary>
        /// The reservation has been confirmed.
        /// </summary>
        Confirmed,

        /// <summary>
        /// The reservation is waiting for approval or processing.
        /// </summary>
        Waiting,

        /// <summary>
        /// The reservation request has been denied.
        /// </summary>
        Denied
    }
}
