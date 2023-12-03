/// <summary>
/// Created by Alexander Carvalho, Class to store information about
/// the geolocation of a listing
/// </summary>

namespace PARKIT_enterprise_final.Models
{
    public class MapPoint
    {
        public Guid id { get; set; } // primary key
        public double latitude { get; set; } // latitude of the listing
        public double longitude { get; set; } // longitude of the listing
        public string thumbnail { get; set; } // thumbnail (image) of the listing
        public string street { get; set; } // street of the listing
        public string city { get; set; } // city of the listing
        public string zip { get; set; } // zip code of the listing
        public double price { get; set; } // price of the listing

    }
}
