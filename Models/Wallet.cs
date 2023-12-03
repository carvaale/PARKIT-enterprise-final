using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    public class Wallet
    {
        [Key]
        public Guid WalletId { get; set; }
        public string? CardNumber { get; set; }
        public string? CardHolderName { get; set; }


/*        public double Balance { get; set; }
*//*        public void CheckBalance() { }
        public void Deposit(double amount) { }
        public void Withdraw(double amount) { }*/
    }
}
