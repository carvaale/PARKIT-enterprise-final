using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.ViewModels;
using System.Reflection;

namespace PARKIT_enterprise_final.Controllers
{
    public class BookingController : Controller
    {
        private readonly IListingsProvider _listingProvider;
        private readonly IBookingProvider _bookingProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingController(IListingsProvider listingsProvider, IBookingProvider bookingProvider, IHttpContextAccessor httpContextAccessor)
        {
            _listingProvider = listingsProvider;
            _bookingProvider = bookingProvider;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public IActionResult BookListing(Guid Id)
        {
            //Retreives single listing through Id
            Listing listing = _listingProvider.GetById(Id);


            var viewModel = new ListingDetailsViewModel
            {
                Listing = listing,
                Booking = new Booking()
            };

            return View(viewModel);

        }

        [HttpPost]
        public IActionResult BookListing([Bind(Prefix = "Booking")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                string userId = _httpContextAccessor.HttpContext.Session.GetString("LoginUser");

                userId = "B8B72E7F-8B1D-4D20-AC8A-E44864022500";
                booking = _bookingProvider.CreateBooking(booking, userId);

                _bookingProvider.AddBooking(booking);

                return RedirectToAction("Account", "Home");
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
