namespace PARKIT_enterprise_final.Models
{
    public class Buyer
    {
        public Guid Id { get; set; }
        public List<Car> Cars { get; set; }
        public void BuyListing(Listing listing) { }
        public void AddCar(Car car) { }
        public void RemoveCar(Car car) { }
    }
}
