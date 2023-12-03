using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.ViewModels
{
    /// <summary>
    /// Author: Syed Taha Faisal
    /// Description: ViewModel for representing the data required for account-related views.
    /// This includes details about the user, their bookings, and their wallet.
    /// </summary>
    public class AccountsViewModel
    {
        public User User { get; set; }
        public List<Booking> BookingList { get; set; }
        public Wallet Wallet { get; set; }
    }
}
