using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class BookingOperations : IBookingProvider
    {
        private readonly PARKITDBContext _dbContext;

        public BookingOperations(PARKITDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _dbContext.Bookings.Include(b => b.Listing).ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        {
            return await _dbContext.Bookings.Include(b => b.Listing).FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Update(booking);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(Guid bookingId)
        {
            var booking = await GetBookingByIdAsync(bookingId);
            if (booking != null)
            {
                _dbContext.Bookings.Remove(booking);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
