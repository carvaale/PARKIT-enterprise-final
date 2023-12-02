using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    // [Owned]
    public class Address
    {
        [Key]
        [Required]
        public Guid AddressId { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }

        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }
}
