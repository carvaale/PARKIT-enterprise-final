using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PARKIT_enterprise_final.Models
{
    
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] ImageData { get; set; }

        public Guid ListingId { get; set; }
        [ForeignKey("ListingId")]
        public Listing Listing { get; set; }



        
    }
}
