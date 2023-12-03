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
        [MaxLength(100)]
        [RegularExpression(@"^\d+\s+([a-zA-Z]+\s*)+$", ErrorMessage = "Invalid Street Address")]
        public string? StreetAddress { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[A-Za-z\s-]+$", ErrorMessage = "Invalid City Name")]
        public string? City { get; set; }
        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$", ErrorMessage = "Invalid Canadian Zip Code")]
        public string? ZipCode { get; set; }

        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }
}
