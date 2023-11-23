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
                        ZipCode = listing["Address.ZipCode"]
                    },
                    IsAvailable = true,
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
