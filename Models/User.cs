using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Address? Address { get; set; }
        public Wallet? Wallet { get; set; }

        
        public List<Listing>? Listings { get; set; }
    }
}
