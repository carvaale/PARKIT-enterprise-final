namespace PARKIT_enterprise_final.Models
{
    public class Listing
    {
        public int Id { get; set; } 
        public Address Address { get; set; }
        public bool IsAvailable { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public double Price { get; set; }

        public List<Image> Images { get; set; }
    }
}
