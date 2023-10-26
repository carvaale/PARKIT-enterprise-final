using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Repository;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class MapOperations : IMapProvider
    {
        private readonly ListingDBContext _context;
        Map map;

        public MapOperations(ListingDBContext context)
        {
            _context = context;
            map = new Map();
        }

        public Map LoadMap()
        {
            map.Listings = _context.Listings.ToList();
            return map;
        }
    }
}
