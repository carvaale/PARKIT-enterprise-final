using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.ViewModels
{
    /// <summary>
    /// Author: Syed Taha Faisal
    /// Description: ViewModel representing the data required for views that display the details of a listing.
    /// It includes the listing details, associated booking information, and the user's wallet data.
    /// </summary>
    public class ListingDetailsViewModel
    {
        public Listing Listing { get; set; }

        public Booking Booking { get; set; }
        public Wallet Wallet { get; set; }

    }
}
