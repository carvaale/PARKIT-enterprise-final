using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Controllers
{
    public class ListingController : Controller
    {
        private readonly IListingsProvider _listingProvider;

        public ListingController(IListingsProvider listingsProvider)
        {
            _listingProvider = listingsProvider;
        }
        public IActionResult Index()
        {
            return RedirectToAction("AllListings");
        }

        [HttpGet]
        public IActionResult AllListings()
        {
            return View(_listingProvider.GetListings());
        }

        [HttpGet]
        public IActionResult DeleteListing(Guid id)
        {
            return View(_listingProvider.GetById(id));
        }

        [HttpPost]
        public IActionResult DeleteListing(Listing listing)
        {
            _listingProvider.DeleteListing(listing);
            return RedirectToAction("AllListings");
        }

        [HttpGet]
        public IActionResult ListingDetails(Guid id)
        {
            Listing listing = _listingProvider.GetById(id);
            ViewBag.Images = _listingProvider.GetImagesByListingId(id);

            return View(listing);
        }


        [HttpGet]
        public IActionResult UpdateListing(Guid id)
        {
            Listing listing = _listingProvider.GetById(id);
            ViewBag.Images = _listingProvider.GetImagesByListingId(id);

            return View(listing);
        }
        [HttpPost]
        public IActionResult UpdateListing(IFormCollection listing)
        {

            _listingProvider.UpdateListing(listing); ;

            return RedirectToAction("AllListings");
        }

        [HttpGet]
        public IActionResult CreateListing()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateListing(IFormCollection listing)
        {
            if (ModelState.IsValid)
            {
                // Wallet, Address and User are all temporary until we figure out how to get the user id from the session, this is just for testing purposes
                Wallet w1 = new Wallet 
                { 
                    //WalletId = Guid.NewGuid(), 
                    CardHolderName = "test", 
                    CardNumber = "test"
                };

                Address a1 = new Address
                {
                    //AddressId = Guid.NewGuid(),
                    City = "Test",
                    StreetAddress = "Test",
                    ZipCode = "Test",
                    Latitude = "test",
                    Longitude = "test",
                };
                User u1 = new User
                {
                    //Id = Guid.NewGuid(),
                    FirstName = "Bob",
                    LastName = "DaBuilder",
                    Email = "Test",
                    Password = "Test",
                    Phone = "000-111-2222",
                    Username = "Bob",
                    Wallet = w1,
                    Address = a1
                };

                _listingProvider.AddListing(listing, u1);
                return RedirectToAction("AllListings");


            }
            return View();
        }
    }
}
