/// <summary>
/// Created by Frank Carusi, funcitonality for all listing related operations
/// </summary>
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Utils;
using System.Reflection;

namespace PARKIT_enterprise_final.Models.Operations
{
    /// <summary>
    /// Functionality for all listing related operations
    /// </summary>
    public class ListingOperations : IListingsProvider
    {
        private readonly PARKITDBContext _context; // Database context for interation with the database 
        private readonly GeocodeApi _geocodeApi;  // Added by Alex Carvahlo, used to get the longitude and latitude from the address

        /// <summary>
        /// Inject the database context and service for converting address into longitude and latitude into the class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="geocodeApi"></param>
        public ListingOperations(PARKITDBContext context, GeocodeApi geocodeApi)
        {
            _context = context;
            _geocodeApi = geocodeApi;
        }

        /// <summary>
        /// Will add and image to the database
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Image AddImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
            return image;
        }

        /// <summary>
        /// Will add a listing to the listings table and all images associated with the listing to the images table
        /// </summary>
        /// <param name="listing"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Listing AddListing(IFormCollection listing, string userId)
        {
            // Convert the string value of IsAvailable from the form to a bool type
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


            // Get the longitude and latitude from the address (Added by Alex Carvahlo)
            LatLng latLng = _geocodeApi.GetGeocode(listing["Address.StreetAddress"]).Result;

            // Create a new listing object from the form data
            Listing newListing = new Listing
            {
                User = _context.Users.Find(Guid.Parse(userId)), // get the user from the database using the userId
                
                // Get the address from the form and create a new address object from it
                Address = new Address 
                {
                    StreetAddress = listing["Address.StreetAddress"], 
                    City = listing["Address.City"],
                    ZipCode = listing["Address.ZipCode"],
                    Longitude = latLng.Lng,
                    Latitude = latLng.Lat

                },

                IsAvailable = IABool, // was converted from string to bool above

                // Set the remaining properties of the listing object from the form data
                StartTime = TimeSpan.Parse(listing["StartTime"]),
                EndTime = TimeSpan.Parse(listing["EndTime"]),
                Price = double.Parse(listing["Price"])
            };

            var files = listing.Files; // get the files from the form

            List<Image> images = new List<Image>(); // Create a list of images to be added to the listing
            foreach (var file in files) // loop through the files and add them to the list of images
            {
                using (var ms = new MemoryStream()) // convert the file to a byte array and add it to the list of images
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    images.Add(new Image { ImageData = fileBytes, ListingId = newListing.Id });
                }
            }
            newListing.Images = images; // set the images property of the listing to the list of images

            _context.Listings.Add(newListing);  // add the listing to the database
            _context.SaveChanges(); // save the changes to the database
            return newListing; // return the listing
        }

        /// <summary>
        /// Will delete a given listing from the database
        /// </summary>
        /// <param name="listing"></param>
        public void DeleteListing(Listing listing)
        {
            _context.Listings.Remove(listing);
            _context.SaveChanges();
        }

        /// <summary>
        /// Will return a listing from the database with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Listing GetById(Guid id)
        {
            Listing listing = _context.Listings.Find(id);
            listing.User = _context.Users.Find(listing.UserId); // get the user associated with the listing and set it to the listing's user property
            return listing;
            
        }

        /// <summary>
        /// Will return a list of base64 strings of all images associated with the listing given listing id, this can then be passed to the view through the ViewBag to be displayed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetImagesByListingId(Guid id)
        {
            Listing listing = _context.Listings.Find(id); // get the listing from the database using the id
            List<string> ImageStrings = new List<string>(); // create a list of strings to hold the base64 strings of the images
            if (listing.Images != null) // if the listing has images
            {
                foreach (var image in listing.Images) // loop through the images and convert them to base64 strings and add them to the list of strings
                {
                    ImageStrings.Add(Convert.ToBase64String(image.ImageData));
                }

            }
            return ImageStrings; // return the list of strings
        }

        /// <summary>
        /// Will return a list of all listings in the database that are set to available, will be used to display all listings that are available to be booked on the map
        /// </summary>
        /// <returns></returns>
        public List<Listing> GetListings()
        {
            List<Listing> listings = _context.Listings.Where(l => l.IsAvailable == true).ToList(); // Only return listings that are available
            return listings;
        }

        /// <summary>
        /// Will return a list of all listings in the database that are associated with the given user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Listing> GetUserListings(string userId)
        {
            List<Listing>? listings = _context.Listings.Where(l => l.UserId == Guid.Parse(userId)).ToList();
            if (listings != null)
            {
                return listings;
            }
            return null;
        }

        /// <summary>
        /// Will return a base64 string of the first image associated with the listing, this can then be passed to the view through the ViewBag to be displayed.
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

        /// <summary>
        /// Will update a listing in the database with the given input, if images are updated for the listing, the old images will be deleted and the new images will be added to the database.
        /// </summary>
        /// <param name="listing"></param>
        /// <returns></returns>
        public Listing UpdateListing(IFormCollection listing)
        {
            // Convert the string value of IsAvailable from the form to a bool type
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

            // Set the properties of the listing to the updated values from the form
            newListing.IsAvailable = IABool;
            newListing.StartTime = TimeSpan.Parse(listing["StartTime"]);
            newListing.EndTime = TimeSpan.Parse(listing["EndTime"]);
            newListing.Price = double.Parse(listing["Price"]);

            var files = listing.Files; // get the files from the form
            List<Image> images = new List<Image>(); // Create a list of images to be added to the listing

            if (files.Count > 0) // if there are files in the form
            {

                // Loop through the files and add them to the list of images
                foreach (var file in files)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        images.Add(new Image { ImageData = fileBytes, ListingId = newListing.Id });
                    }
                }
                newListing.Images = images; // set the images property of the listing to the list of images
            }

            _context.Listings.Update(newListing);
            _context.SaveChanges();
            return newListing;
        }
    }
}
