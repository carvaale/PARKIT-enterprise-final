/// <summary>
/// Created by Ruiyan Shi 
/// for the Wallet object, used for store wallet information, validation when register
/// </summary>
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    public class Wallet
    {
        [Key]
        public Guid WalletId { get; set; } // primary key
        [Required]
        [MaxLength(30)]
        [RegularExpression(@"^([0-9 \s]+)$", ErrorMessage = "Invalid Card Number")]
        public string? CardNumber { get; set; } // store the card number
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9 \.\s]+)$", ErrorMessage = "Invalid Card Holder Name")]
        public string? CardHolderName { get; set; } // the name of the card holder
    }
}
