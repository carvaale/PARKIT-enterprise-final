using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;

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

        public Listing UpdateListing(Listing listing)
        {
            _context.Listings.Update(listing);
            _context.SaveChanges();
            return listing;
        }
    }
}
