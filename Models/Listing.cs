namespace PARKIT_enterprise_final.Models
{
    public class Listing
    {
        public int Id { get; set; } 
        public Address Address { get; set; }
        public bool IsAvailable { get; set; }
    }
}
