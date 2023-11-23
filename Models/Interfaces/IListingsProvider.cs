namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IListingsProvider
    {
        Listing AddListing(Listing listing);

        Listing UpdateListing(Listing listing);

        Image AddImage(Image image);


    }
}
