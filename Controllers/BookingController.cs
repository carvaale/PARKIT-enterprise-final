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

        public BookingController(IListingsProvider listingsProvider, IBookingProvider bookingProvider)
        {
            _listingProvider = listingsProvider;
            _bookingProvider = bookingProvider;
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
        public IActionResult BookListing(ListingDetailsViewModel model)
        {
            // Calculate and set the total cost of the booking
            model.Booking.TotalCost = _bookingProvider.CalculateTotalCost(model.Booking);

            _bookingProvider.AddBooking(model.Booking);

            return RedirectToAction("Account", "Home");



/*            if (ModelState.IsValid)
            {
                Booking booking = model.Booking;

                // Calculate and set the total cost of the booking
                booking.TotalCost = _bookingProvider.CalculateTotalCost(booking);

                _bookingProvider.AddBooking(booking);

                return RedirectToAction("Account", "Home");
            }

            else
            {
                Booking booking = model.Booking;

                Console.WriteLine($"Booking ID: {booking.Id}");
                Console.WriteLine($"License Plate: {booking.LicensePlate}");
                Console.WriteLine($"Start Time: {booking.StartTime}");
                Console.WriteLine($"End Time: {booking.EndTime}");

                return RedirectToAction("Index", "Home");
            }*/
        }
    }
}
