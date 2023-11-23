namespace PARKIT_enterprise_final.Models
{
    public class Seller
    {
        public Guid Id { get; set; }
        public List<Listing>? Listings { get; set; }
        public void AddListing(Listing listing) { }
        public void RemoveListing(Listing listing) { }
    }
}
