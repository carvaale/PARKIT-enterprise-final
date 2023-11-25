using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    [Owned]
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
