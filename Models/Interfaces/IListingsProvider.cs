﻿/// <summary>
/// Created by Frank Carusi, interface for all listing related operations, for functionality documentation refer to the ListingsProvider class
/// </summary>
namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IListingsProvider
    {
        Listing AddListing(IFormCollection listing, string userId);

        Listing UpdateListing(IFormCollection listing);

        void DeleteListing(Listing listing);

        Image AddImage(Image image);

        Listing GetById(Guid id);

        List<Listing> GetUserListings(string userId);
        List<Listing> GetListings();

        string GetSingleImageByListingId(Guid id);

        List<string> GetImagesByListingId(Guid id);
    }
}
