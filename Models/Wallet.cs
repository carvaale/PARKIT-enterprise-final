using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    public class Wallet
    {
        [Key]
        public Guid WalletId { get; set; }
        [Required]
        [MaxLength(30)]
        [RegularExpression(@"^([0-9 \s]+)$", ErrorMessage = "Invalid Card Number")]
        public string? CardNumber { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9 \.\s]+)$", ErrorMessage = "Invalid Card Holder Name")]
        public string? CardHolderName { get; set; }


/*        public double Balance { get; set; }
*//*        public void CheckBalance() { }
        public void Deposit(double amount) { }
        public void Withdraw(double amount) { }*/
    }
}
