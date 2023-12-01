using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class BookingOperations : IBookingProvider
    {
        private readonly PARKITDBContext _dbContext;
        private readonly IListingsProvider _listingProvider;

        public BookingOperations(PARKITDBContext dbContext, IListingsProvider listingProvider)
        {
            _dbContext = dbContext;
            _listingProvider = listingProvider;
        }

        public void AddBooking(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
        }

        public Booking GetBooking(Guid Id)
        {
               return _dbContext.Bookings.Find(Id);
        }

        public double CalculateTotalCost(Booking booking)
        {
            // Retrieve the related listing using fk
            Listing listing = _listingProvider.GetById(booking.ListingId);

            // Calculate the total hours as a double
            double totalHours = (booking.EndTime - booking.StartTime).TotalHours;


            // Calculate the total cost
            double totalCost = totalHours * 20;

            return totalCost;
        }
    }
}
