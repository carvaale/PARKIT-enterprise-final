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

        public IActionResult Index()
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

            List<Listing> listings = new List<Listing>() 
            { 
                new Listing { 
                    Address = new Address { 
                        StreetAddress = "1234 Main St", 
                        City = "Seattle", 
                        ZipCode = "98101", 
                        Latitude = "43.65804053299989", 
                        Longitude = "-79.4349384105354"
                    }, 
                    IsAvailable = true, 
                    StartTime = TimeSpan.Parse("10:00"), 
                    EndTime = TimeSpan.Parse("12:00"), 
                    Price = 10.00 
                },
                new Listing {
                    Address = new Address {
                        StreetAddress = "1234 Main St",
                        City = "Seattle",
                        ZipCode = "98101",
                        Latitude = "43.64804053299989",
                        Longitude = "-79.4349384105354 "
                    },
                    IsAvailable = true,
                    StartTime = TimeSpan.Parse("10:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Price = 10.00
                },
                new Listing {
                    Address = new Address {
                        StreetAddress = "1234 Main St",
                        City = "Seattle",
                        ZipCode = "98101",
                        Latitude = "43.65804053299989",
                        Longitude = "-79.4249384105354"
                    },
                    IsAvailable = true,
                    StartTime = TimeSpan.Parse("10:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Price = 10.00
                },
            };
 
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