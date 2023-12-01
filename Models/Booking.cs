using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
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
/*        [ForeignKey("UserID")]
*//*        public Guid UserID { get; set; }*/
    }

}