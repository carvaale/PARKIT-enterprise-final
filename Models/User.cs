/// <summary>
/// Created by Ruiyan Shi 
/// for the User object, used for store user, validation when register and login
/// </summary>
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } // primary key
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9]+)$", ErrorMessage = "Invalid UserName, should only contains letters and numbers")]
        public string Username { get; set; } // Username for login
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password should contains at least one alphabetical character, one digit, and one special character fomr '@$!%*?&'")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } // user's password
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid FirstName")]
        public string? FirstName { get; set; } // User's first name
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid LastName")]
        public string? LastName { get; set; } // user's last name
        [Required] 
        [EmailAddress]
        [MaxLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\-]+@[a-zA-Z0-9 \.\&\-]+\.[a-zA-Z0-9 \.\&\-]+)$", ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string? Email { get; set; } // user's email to reveice emails in the future
        [Required]
        [Phone]
        [RegularExpression(@"^([\+]?[0-9]+)", ErrorMessage = "Invalid Phone Number")]
        public string? Phone { get; set; } // user's phone number
        [Required]
        public Address? Address { get; set; } // store the address object

        public Guid? WalletId { get; set; } // store walletId to track which wallet belongs to user

        [Required]
        public Wallet? Wallet { get; set; } // store wallet 

        
        public List<Listing>? Listings { get; set; } // store the listings that this user lists
    }
}
