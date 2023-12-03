using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.ViewModels;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PARKIT_enterprise_final.Controllers
{
    public class BookingController : Controller
    {
        private readonly IListingsProvider _listingProvider;
        private readonly IBookingProvider _bookingProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWalletProvider _walletProvider;

        public BookingController(IListingsProvider listingsProvider, IBookingProvider bookingProvider, IHttpContextAccessor httpContextAccessor, IWalletProvider walletProvider)
        {
            _listingProvider = listingsProvider;
            _bookingProvider = bookingProvider;
            _httpContextAccessor = httpContextAccessor;
            _walletProvider = walletProvider;
        }


        [HttpGet]
        public IActionResult BookListing(Guid Id)
        {
            string userId = _bookingProvider.getUserId();

            if (userId == "false")
            {
                return RedirectToAction("Login", "User");
            }

            //Retreives single listing through Id
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

        [HttpPost]
        public IActionResult BookListing([Bind(Prefix = "Booking")] Booking booking)
        {
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
            if (ModelState.IsValid)
            {
                string userId = _bookingProvider.getUserId();

                if (userId == "false") 
                {
                    return RedirectToAction("Login", "User");
                }
                booking = _bookingProvider.CreateBooking(booking, userId);

                _bookingProvider.AddBooking(booking,listing);

                return RedirectToAction("Account", "Account");
            }
            else
            {
                string userId = _bookingProvider.getUserId();
                if (userId == "false")
                {
                    return RedirectToAction("Login", "User");
                }
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
