using PARKIT_enterprise_final.Models.DBContext;

namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IBookingProvider
    {

        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);

        double CalculateTotalCost(Booking booking);
    }
}
