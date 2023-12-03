/// <summary>
/// Created by Frank Carusi. Image object model
/// </summary>
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PARKIT_enterprise_final.Models
{
    
    public class Image
    {
        [Key]
        public Guid Id { get; set; }// primary key
        public byte[] ImageData { get; set; } // image data in byte array so it can be stored in the database

        public Guid ListingId { get; set; } // foreign key to the listing table
        [ForeignKey("ListingId")]
        public Listing Listing { get; set; } // listing that the image belongs to



        
    }
}
