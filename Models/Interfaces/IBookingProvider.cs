namespace PARKIT_enterprise_final.Models.Interfaces
{
    /// <summary>
    /// Author: Syed Taha Faisal
    /// Description: Interface defining the contract for booking-related operations within the system.
    /// This includes methods for adding, creating, retrieving bookings, calculating costs, and getting user IDs (Used as Foriegn Keys).
    /// </summary>
    public interface IBookingProvider
    {
        void AddBooking(Booking booking, Listing listing);

        Booking CreateBooking(Booking booking, string userId);

        List<Booking> GetAllBookings(Guid id);

        double CalculateTotalCost(Booking booking);

        string getUserId();
    }
}
