/// <summary>
/// Created by Ruiyan Shi 
/// for the User login. can only access and validate the user input for username and password
/// </summary>
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9]+)$", ErrorMessage = "Invalid UserName, should only contains letters and numbers")]
        public string UserName { get; set; } // the username from user's login input

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password should contains at least one alphabetical character, one digit, and one special character fomr '@$!%*?&'")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } // the password from user's login input
    }
}
