using PARKIT_enterprise_final.Models.DBContext;

namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IBookingProvider
    {

        void AddBooking(Booking booking);

        Booking CreateBooking(Booking booking, string userId);

        List<Booking> GetAllBookings(Guid id);

        double CalculateTotalCost(Booking booking);

        string getUserId();
    }
}
