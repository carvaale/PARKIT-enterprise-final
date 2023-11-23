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
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AllListings()
        {
            return View(_listingProvider.GetListings());
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
                Listing newListing = new Listing
                {
                    Address = new Address
                    {
                        StreetAddress = listing["Address.StreetAddress"],
                        City = listing["Address.City"],
                        ZipCode = listing["Address.ZipCode"],
                        Longitude = "Test", // going to try to get the longitude and latitude from the address
                        Latitude = "Test"
                    },
                    IsAvailable = listing["IsAvailable"].Count > 0 ? true : false, // this needs to be fixed
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
                return RedirectToAction("Index");


            }
            return View();
        }
    }
}
