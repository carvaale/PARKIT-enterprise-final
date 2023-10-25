namespace PARKIT_enterprise_final.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Listing Listing { get; set; }
        public Car Car { get; set; }
        public Wallet Wallet { get; set; }
        public Guid BuyerID { get; set; }
        public Guid SellerID { get; set; }
        public void Pay() { }
        public void Complete() { }
    }
}
