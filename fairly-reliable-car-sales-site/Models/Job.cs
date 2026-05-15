using System;
using System.ComponentModel.DataAnnotations;

namespace FairlyReliableCarSalesSite.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The job title is required.")]
        [StringLength(100, ErrorMessage = "The title must be less than 100 characters.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "The job description is required.")]
        [StringLength(1000, ErrorMessage = "The description must be less than 1000 characters.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "The image URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL for the image.")]
        [StringLength(255, ErrorMessage = "The image URL must be less than 255 characters.")]
        [Display(Name = "Image URL")]
        public required string ImageUrl { get; set; }
    }
}
