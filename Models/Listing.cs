using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PARKIT_enterprise_final.Models
{
    public class Listing
    {

        [Key]
        public Guid Id { get; set; }
        public Address Address { get; set; }
        public bool IsAvailable { get; set; }

        public bool IsBooked { get; set; } = false;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        //price per hour
        public double Price { get; set; }

        public List<Image>? Images { get; set; }

        [FromForm(Name = "Images")]
        [NotMapped]
        public IFormFileCollection? Files { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; } // is only nullable until we figure out how to get the user id from the session
    }
}
