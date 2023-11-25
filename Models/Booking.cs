using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Listing")]
        public Guid ListingId { get; set; }
        public Listing Listing { get; set; }
        //Infer SellerID from Listing.UserId and BuyerID from logged in buyerID
        // public Guid BuyerID { get; set; }
        // public Guid SellerID { get; set; }

        //public Wallet Wallet { get; set; } <- Infer from  User.Wallet

        public string LicensePlate { get; set; }
        public void Pay() { }
        public void Complete() { }
    }

}