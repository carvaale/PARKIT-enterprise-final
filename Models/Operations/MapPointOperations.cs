/// <summary>
/// Created by Alexander Carvalho, functionality for all map points
/// </summary>


using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Models.Operations
{
    /// <summary>
    /// Created by Alexander Carvalho, functionality for all map points
    /// </summary>
    public class MapPointOperations : IMapPointProvider
    {
        private readonly IListingsProvider _listingProvider;

        public MapPointOperations(IListingsProvider listingProvider)
        {
            _listingProvider = listingProvider;
        }

        /// <summary>
        /// Get all available listings and convert them to map points
        /// </summary>
        public List<MapPoint> GetMapPoints()
        {
            List<MapPoint> mapPoints = new List<MapPoint>(); // Create an empty list of map points
            List<Listing> listings = _listingProvider.GetListings(); // Get all listings

            foreach (Listing listing in listings) // Convert each listing to a map point
            {
                MapPoint mapPoint = new MapPoint
                {
                    id = listing.Id,
                    latitude = Convert.ToDouble(listing.Address.Latitude),
                    longitude = Convert.ToDouble(listing.Address.Longitude),
                    thumbnail = Convert.ToBase64String(listing.Images.First().ImageData),
                    street = listing.Address.StreetAddress,
                    city = listing.Address.City,
                    zip = listing.Address.ZipCode,
                    price = listing.Price
                };

                mapPoints.Add(mapPoint);
            }

            return mapPoints;
        }
    }
}
