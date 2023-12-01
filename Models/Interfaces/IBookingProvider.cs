using PARKIT_enterprise_final.Models.DBContext;

namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IBookingProvider
    {

        void AddBooking(Booking booking);

        Booking GetBooking(Guid id);

        double CalculateTotalCost(Booking booking);
    }
}
