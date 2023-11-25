using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;
using System.Reflection;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class ListingOperations : IListingsProvider
    {
        private readonly PARKITDBContext _context;

        public ListingOperations(PARKITDBContext context)
        {
            _context = context;
        }

        public Image AddImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
            return image;
        }

        public Listing AddListing(Listing listing)
        {
            _context.Listings.Add(listing);
            _context.SaveChanges();
            return listing;
        }

        public void DeleteListing(Listing listing)
        {
            _context.Listings.Remove(listing);
            _context.SaveChanges();
        }

        public Listing GetById(Guid id)
        {
            Listing listing = _context.Listings.Find(id);
            listing.User = _context.Users.Find(listing.UserId);
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

        public Listing UpdateListing(Listing listing)
        {
            _context.Listings.Update(listing);
            _context.SaveChanges();
            return listing;
        }
    }
}
