using Newtonsoft.Json;
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Models.Operations
{
    /// <summary>
    /// Author: Syed Taha Faisal
    /// Description: This class handles the operations related to bookings, implementing the IBookingProvider interface.
    /// It includes methods for adding, creating, and managing bookings within the system.
    /// </summary>
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

        /// <summary>
        /// Adds a booking to the database and marks the listing as booked.
        /// </summary>
        public void AddBooking(Booking booking, Listing listing)
        {
            _dbContext.Bookings.Add(booking);
            listing.IsBooked = true;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Creates a booking for a specific user. 
        /// This method involves generating a new booking instance with the given details and associating it with the specified user.
        /// </summary>
        public Booking CreateBooking(Booking booking, string userId) 
        {
            booking.TotalCost = CalculateTotalCost(booking);
            booking.UserID = Guid.Parse(userId);
            return booking;
        }

        /// <summary>
        /// Retrieves all bookings associated with a given user ID.
        /// This method is essential for fetching a list of all bookings made by a specific user, aiding in user booking management.
        /// </summary>
        public List<Booking> GetAllBookings(Guid Id)
        {
            return _dbContext.Bookings.Where(b => b.UserID == Id).Take(5).ToList();
        }

        /// <summary>
        /// Calculates the total cost of a booking.
        /// This method is responsible for determining the financial aspect of a booking, which involves factors like duration & listing price.
        /// </summary>
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

        /// <summary>
        /// Retrieves the user ID from the current HTTP context.
        /// This method is crucial for identifying the currently logged-in user, allowing for user-specific operations and data retrieval. Helpful in de-cluttering buisness logic within controllers.
        /// </summary>
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
