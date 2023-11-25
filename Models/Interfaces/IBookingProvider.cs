namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IBookingProvider
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(Guid bookingId);
        Task CreateBookingAsync(Booking booking);
    }
}
