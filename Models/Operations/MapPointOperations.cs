using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class MapPointOperations : IMapPointProvider
    {
        private readonly IListingsProvider _listingProvider;

        public MapPointOperations(IListingsProvider listingProvider)
        {
            _listingProvider = listingProvider;
        }

        public List<MapPoint> GetMapPoints()
        {
            List<MapPoint> mapPoints = new List<MapPoint>();

            List<Listing> listings = _listingProvider.GetListings();

            foreach (Listing listing in listings)
            {
                MapPoint mapPoint = new MapPoint
                {
                    id = listing.Id,
                    latitude = Convert.ToDouble(listing.Address.Latitude),
                    longitude = Convert.ToDouble(listing.Address.Longitude),
                    thumbnail = listing.Images.First(),
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
