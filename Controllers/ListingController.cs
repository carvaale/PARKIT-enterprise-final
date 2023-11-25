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
            return View(_listingProvider.GetById(id));
        }


        [HttpGet]
        public IActionResult UpdateListing(Guid id)
        {
            return View(_listingProvider.GetById(id));
        }
        [HttpPost]
        public IActionResult UpdateListing(Listing listing)
        {
            if (ModelState.IsValid)
            {
                _listingProvider.UpdateListing(listing);
                return RedirectToAction("Index");
            }
            return View();
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
                Wallet w1 = new Wallet 
                { 
                    WalletId = Guid.NewGuid(), 
                    cardHolderName = "test", 
                    cardNumber = "test" 
                };

                Address a1 = new Address
                {
                    AddressId = Guid.NewGuid(),
                    City = "Test",
                    StreetAddress = "Test",
                    ZipCode = "Test",
                    Latitude = "test",
                    Longitude = "test",
                };
                User u1 = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "Test",
                    Password = "Test",
                    Phone = "Test",
                    Username = "Test",
                    Wallet = w1,
                    Address = a1
                };
                string IAString = listing["IsAvailable"];
                bool IABool; 
                if (IAString == "false")
                {
                    IABool = false;
                }
                else
                {
                    IABool = true;
                }
                Listing newListing = new Listing
                {
                    User = u1,
                    Address = new Address
                    {
                        StreetAddress = listing["Address.StreetAddress"],
                        City = listing["Address.City"],
                        ZipCode = listing["Address.ZipCode"],
                        Longitude = "Test", // going to try to get the longitude and latitude from the address
                        Latitude = "Test"
                    },
                    IsAvailable = IABool,
                    StartTime = TimeSpan.Parse(listing["StartTime"]),
                    EndTime = TimeSpan.Parse(listing["EndTime"]),
                    Price = double.Parse(listing["Price"])
                };

                var files = listing.Files;

                List<Image> images = new List<Image>();
                foreach (var file in files)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        images.Add(new Image { ImageData = fileBytes, ListingId = newListing.Id });
                    }
                }
                newListing.Images = images;
                _listingProvider.AddListing(newListing);
                return RedirectToAction("AllListings");


            }
            return View();
        }
    }
}
