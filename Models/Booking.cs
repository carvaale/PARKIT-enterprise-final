using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    /// <summary>
    /// Author: Syed Taha Faisal
    /// Description: Represents a booking in the system. This class includes properties that define the characteristics of a booking, such as timing, cost, and associated user and listing IDs.
    /// </summary>
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double TotalCost { get; set; }
        [ForeignKey("ListingId")]
        public Guid ListingId { get; set; }
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
    }

}