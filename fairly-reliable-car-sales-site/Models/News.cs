using System;
using System.ComponentModel.DataAnnotations;

namespace FairlyReliableCarSalesSite.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The news title is required.")]
        [StringLength(100, ErrorMessage = "The title must be less than 100 characters.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "The summary is required.")]
        [StringLength(250, ErrorMessage = "The summary must be less than 250 characters.")]
        public required string Summary { get; set; }

        [Required(ErrorMessage = "The details are required.")]
        [StringLength(2000, ErrorMessage = "The details must be less than 2000 characters.")]
        public required string Details { get; set; }

        [Required(ErrorMessage = "The image URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL for the image.")]
        [StringLength(255, ErrorMessage = "The image URL must be less than 255 characters.")]
        [Display(Name = "Image URL")]
        public required string ImageUrl { get; set; }
    }
}
