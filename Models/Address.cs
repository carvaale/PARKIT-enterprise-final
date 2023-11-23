using Microsoft.EntityFrameworkCore;

namespace PARKIT_enterprise_final.Models
{
    [Owned]
    public class Address
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
