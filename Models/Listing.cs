/// <summary>
/// Created by Frank Carusi, Listing object model
/// </summary>
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PARKIT_enterprise_final.Models
{
    public class Listing
    {

        [Key]
        public Guid Id { get; set; } // primary key
        [Required (ErrorMessage = "Address is required")]
        public Address? Address { get; set; } // address of the listing
        [Required]
        public bool IsAvailable { get; set; } // is the listing available for booking
        [Required]
        public bool IsBooked { get; set; } = false; // is the listing currently booked, set to false by default until the listing is booked
        [Required(ErrorMessage = "StartTime is required")]
        public TimeSpan StartTime { get; set; } // start time of the listing
        [Required(ErrorMessage = "EndTime is required")]
        public TimeSpan EndTime { get; set; } // end time of the listing

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; } // price of the listing per hour

        public List<Image>? Images { get; set; } // list of images for the listing

        [FromForm(Name = "Images")]
        [NotMapped]
        public IFormFileCollection? Files { get; set; } // list of the image files submitted by user when updating or creating a listing

        [Required]
        public Guid UserId { get; set; } // foreign key to the user table
        [ForeignKey("UserId")]
        [Required]
        public User User { get; set; } // user that owns the listing
    }
}
