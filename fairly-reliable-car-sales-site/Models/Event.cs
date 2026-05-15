using System;
using System.ComponentModel.DataAnnotations;

namespace FairlyReliableCarSalesSite.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The event title is required.")]
        [StringLength(100, ErrorMessage = "The title must be less than 100 characters.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "The event date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Event details are required.")]
        [StringLength(1000, ErrorMessage = "Details must be less than 1000 characters.")]
        public required string Details { get; set; }

        [StringLength(255)]
        [Display(Name = "Image URL")]
        [RegularExpression(@"^https?:\/\/.*\.(jpg|jpeg|png|gif)$", ErrorMessage = "Please enter a valid image URL.")]
        public string? ImageUrl { get; set; }
    }
}
