/// <summary>
/// Created by Ruiyan Shi 
/// for the Address object, used for store Address, validation when register
/// </summary>
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    // [Owned]
    public class Address
    {
        [Key]
        [Required]
        public Guid AddressId { get; set; } // primary key
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^\d+\s+([a-zA-Z]+\s*)+$", ErrorMessage = "Invalid Street Address")]
        public string? StreetAddress { get; set; } // store the address
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[A-Za-z\s-]+$", ErrorMessage = "Invalid City Name")]
        public string? City { get; set; } // store the city name
        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$", ErrorMessage = "Invalid Canadian Zip Code")]
        public string? ZipCode { get; set; } // store the zip code

        // Created by other group memebers
        // get the longitude and latitude from api by using address
        // used to put the points on map
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }
}
