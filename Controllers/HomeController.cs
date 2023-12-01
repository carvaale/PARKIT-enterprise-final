using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.Models.Interfaces;
using System.Diagnostics;

namespace PARKIT_enterprise_final.Controllers
{
    public class HomeController : Controller
    {
        private readonly IListingsProvider _listingProvider;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IListingsProvider listingsProvider)
        {
            _logger = logger;
            _listingProvider = listingsProvider;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
/*        [HttpGet]
        public IActionResult CreateListing()
        {
            return View();
        }*/

/*        [HttpPost]
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
        }*/

        public IActionResult Map()
        {            
            List<Listing> listings = _listingProvider.GetListings(); 
            
            ViewData["GMAP_API_KEY"] = Environment.GetEnvironmentVariable("GOOGLE_API_KEY");

            return View(listings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}