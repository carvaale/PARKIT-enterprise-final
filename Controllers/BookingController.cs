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
            //Retreives single listing through Id
            Listing listing = _listingProvider.GetById(Id);

            string userId = _bookingProvider.getUserId();

            if (userId == "false")
            {
                return RedirectToAction("Login", "User");
            }

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
            if (ModelState.IsValid)
            {
                string userId = _bookingProvider.getUserId();

                if (userId == "false") 
                {
                    return RedirectToAction("Login", "User");
                }
                booking = _bookingProvider.CreateBooking(booking, userId);

                _bookingProvider.AddBooking(booking);

                return RedirectToAction("Account", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
