using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class BookingOperations : IBookingProvider
    {
        private readonly PARKITDBContext _dbContext;
        private readonly IListingsProvider _listingProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingOperations(PARKITDBContext dbContext, IListingsProvider listingProvider, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _listingProvider = listingProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddBooking(Booking booking, Listing listing)
        {
            _dbContext.Bookings.Add(booking);
            listing.IsBooked = true;
            _dbContext.SaveChanges();
        }

        public Booking CreateBooking(Booking booking, string userId) 
        {
            booking.TotalCost = CalculateTotalCost(booking);
            booking.UserID = Guid.Parse(userId);
            return booking;
        }

        public List<Booking> GetAllBookings(Guid Id)
        {
            return _dbContext.Bookings.Where(b => b.UserID == Id).Take(5).ToList();
        }

        public double CalculateTotalCost(Booking booking)
        {
            // Retrieve the related listing using fk
            Listing listing = _listingProvider.GetById(booking.ListingId);

            // Calculate the total hours as a double
            double totalHours = (booking.EndTime - booking.StartTime).TotalHours;


            // Calculate the total cost
            double totalCost = totalHours * 20;
            totalCost = Math.Round(totalCost, 2);

            return totalCost;
        }

        public string getUserId()
        {
            string value = _httpContextAccessor.HttpContext.Session.GetString("CurrentUser");

            if (string.IsNullOrEmpty(value))
            {
                return "false";
            }
            User user = JsonConvert.DeserializeObject<User>(value);
            string userId = user.Id.ToString();
            return userId;
        }
    }
}
