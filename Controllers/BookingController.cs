using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.ViewModels;


namespace PARKIT_enterprise_final.Controllers
{
    /// <summary>
    /// Author: Syed Taha Faisal
    /// Description: Controller responsible for handling booking-related actions in the application.
    /// This includes actions like creating, retrieving, and managing bookings.
    /// </summary>
    public class BookingController : Controller
    {
        private readonly IListingsProvider _listingProvider;
        private readonly IBookingProvider _bookingProvider;
        private readonly IWalletProvider _walletProvider;

        public BookingController(IListingsProvider listingsProvider, IBookingProvider bookingProvider, IWalletProvider walletProvider)
        {
            _listingProvider = listingsProvider;
            _bookingProvider = bookingProvider;
            _walletProvider = walletProvider;
        }

        /// <summary>
        /// Handles the GET request to book a listing.
        /// This method retrieves the current user's ID, and if the user is not logged in, it redirects to the login page.
        /// If the user is logged in, it fetches the listing details and the user's wallet information and prepares the view model for the booking page.
        /// </summary>
        /// <returns>The view for booking a listing if the user is logged in, otherwise redirects to the login page.</returns>
        [HttpGet]
        public IActionResult BookListing(Guid Id)
        {
            string userId = _bookingProvider.getUserId();

            if (userId == "false")
            {
                return RedirectToAction("Login", "User");
            }

            Listing listing = _listingProvider.GetById(Id);

            Wallet wallet = _walletProvider.GetWallet(Guid.Parse(userId));

            var viewModel = new ListingDetailsViewModel
            {
                Listing = listing,
                Booking = new Booking(),
                Wallet = wallet
            };

            return View(viewModel);

        }

        /// <summary>
        /// Handles the POST request to book a listing.
        /// This method validates the booking details, such as time validation. If the booking details are valid, it creates and adds the booking.
        /// In case of invalid data, it reloads the booking page with the existing data and error messages.
        /// If the user is not logged in, it redirects to the login page.
        /// </summary>
        /// <returns>Redirects to the account page after successful booking or reloads the booking page with error messages if validation fails. Redirects to the login page if the user is not logged in.</returns>
        [HttpPost]
        public IActionResult BookListing([Bind(Prefix = "Booking")] Booking booking)
        {
            //All this is needed for time validation.
            Listing listing = _listingProvider.GetById(booking.ListingId);

            bool spansOvernight = listing.EndTime < listing.StartTime;
            if (spansOvernight && booking.EndTime < booking.StartTime)
            {
                booking.EndTime = booking.EndTime.Add(new TimeSpan(24, 0, 0));
            }
            if (booking.EndTime <= booking.StartTime)
            {
                ModelState.AddModelError("Booking.EndTime", "End time must be after start time.");
            }

            //After validating time, we can validate the model for submission
            if (ModelState.IsValid)
            {
                string userId = _bookingProvider.getUserId();

                if (userId == "false")
                {
                    return RedirectToAction("Login", "User");
                }
                booking = _bookingProvider.CreateBooking(booking, userId);

                _bookingProvider.AddBooking(booking, listing);

                return RedirectToAction("Account", "Account");
            }
            else
            {
                //If validation fails, we rebuild viewModel and repopulate the View
                string userId = _bookingProvider.getUserId();
                Wallet wallet = _walletProvider.GetWallet(Guid.Parse(userId));

                var viewModel = new ListingDetailsViewModel
                {
                    Listing = listing,
                    Booking = booking,
                    Wallet = wallet
                };

                return View(viewModel);
            }
        }
    }
}