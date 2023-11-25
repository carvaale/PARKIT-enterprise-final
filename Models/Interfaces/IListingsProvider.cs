namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IListingsProvider
    {
        Listing AddListing(Listing listing);

        Listing UpdateListing(Listing listing);

        void DeleteListing(Listing listing);

        Image AddImage(Image image);

        Listing GetById(Guid id);

        List<Listing> GetListings();

        string GetSingleImageByListingId(Guid id);

        List<string> GetImagesByListingId(Guid id);


        


    }
}
