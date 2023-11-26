namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IListingsProvider
    {
        Listing AddListing(IFormCollection listing, User user);

        Listing UpdateListing(IFormCollection listing);

        void DeleteListing(Listing listing);

        Image AddImage(Image image);

        Listing GetById(Guid id);

        List<Listing> GetListings();

        string GetSingleImageByListingId(Guid id);

        List<string> GetImagesByListingId(Guid id);
    }
}
