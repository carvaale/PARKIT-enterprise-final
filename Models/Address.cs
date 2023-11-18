using Microsoft.EntityFrameworkCore;

namespace PARKIT_enterprise_final.Models
{
    [Owned]
    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
}
