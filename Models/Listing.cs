using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PARKIT_enterprise_final.Models
{
    public class Listing
    {

        [Key]
        public Guid Id { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public bool IsBooked { get; set; } = false;
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }

        //price per hour
        [Required]
        public double Price { get; set; }

        public List<Image>? Images { get; set; }

        [FromForm(Name = "Images")]
        [NotMapped]
        public IFormFileCollection? Files { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public User User { get; set; } 
    }
}
