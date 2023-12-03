using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9]+)$", ErrorMessage = "Invalid UserName, should only contains letters and numbers")]
        public string Username { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password should contains at least one alphabetical character, one digit, and one special character fomr '@$!%*?&'")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid FirstName")]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid LastName")]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\-]+@[a-zA-Z0-9 \.\&\-]+\.[a-zA-Z0-9 \.\&\-]+)$", ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"^([\+]?[0-9]+)", ErrorMessage = "Invalid Phone Number")]
        public string? Phone { get; set; }
        [Required]
        public Address? Address { get; set; }
        public Guid? WalletId { get; set; }
        [Required]
        public Wallet? Wallet { get; set; }

        
        public List<Listing>? Listings { get; set; }
    }
}
