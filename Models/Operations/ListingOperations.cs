using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Utils;
using System.Reflection;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class ListingOperations : IListingsProvider
    {
        private readonly PARKITDBContext _context;
        private readonly GeocodeApi _geocodeApi;


        public ListingOperations(PARKITDBContext context, GeocodeApi geocodeApi)
        {
            _context = context;
            _geocodeApi = geocodeApi;
        }

        public Image AddImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
            return image;
        }

        public Listing AddListing(IFormCollection listing, User user)
        {
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


            // Get the longitude and latitude from the address
            LatLng latLng = _geocodeApi.GetGeocode(listing["Address.StreetAddress"]).Result;

            Listing newListing = new Listing
            {
                User = user,
                Address = new Address
                {
                    StreetAddress = listing["Address.StreetAddress"],
                    City = listing["Address.City"],
                    ZipCode = listing["Address.ZipCode"],
                    Longitude = latLng.Lng,
                    Latitude = latLng.Lat

                    // Idk wtf this is doing - Alex
                    //Longitude = "Test", // going to try to get the longitude and latitude from the address 
                    //Latitude = "Test"
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

            _context.Listings.Add(newListing);
            _context.SaveChanges();
            return newListing;
        }

        public void DeleteListing(Listing listing)
        {
            _context.Listings.Remove(listing);
            _context.SaveChanges();
        }

        public Listing GetById(Guid id)
        {
            Listing listing = _context.Listings.Find(id);
            listing.User = _context.UserInfos.Find(listing.UserId);
            return listing;
            
        }

        public List<string> GetImagesByListingId(Guid id)
        {
            Listing listing = _context.Listings.Find(id);
            List<string> ImageStrings = new List<string>();
            if (listing.Images != null)
            {
                foreach (var image in listing.Images)
                {
                    ImageStrings.Add(Convert.ToBase64String(image.ImageData));
                }

            }
            return ImageStrings;
        }

        public List<Listing> GetListings()
        {
            List<Listing> listings = _context.Listings.ToList();
            return listings;
        }

        /// <summary>
        /// Will return a base64 string of the first image associated with the listing, this can then be passed to the view throug the ViewBag to be displayed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSingleImageByListingId(Guid id)
        {
            Listing listing = _context.Listings.Find(id);
            Image selectedImage = listing.Images?.FirstOrDefault();
            string imageString = selectedImage != null ? Convert.ToBase64String(selectedImage.ImageData) : string.Empty;
            return imageString;
        }

        public Listing UpdateListing(IFormCollection listing) // Need to add the ability to remove the images that are already in the db for the provided listing
        {
            
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
            Listing newListing = _context.Listings.Find(Guid.Parse(listing["Id"]));

            newListing.IsAvailable = IABool;
            newListing.StartTime = TimeSpan.Parse(listing["StartTime"]);
            newListing.EndTime = TimeSpan.Parse(listing["EndTime"]);
            newListing.Price = double.Parse(listing["Price"]);

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


            _context.Listings.Update(newListing);
            _context.SaveChanges();
            return newListing;
        }
    }
}
