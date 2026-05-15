using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FairlyReliableCarSalesSite.Models
{
    public partial class Car
    {
        public int CarId { get; set; }

        [Required]
        [Range(1886, 2100, ErrorMessage = "Please enter a valid year.")]
        public short Year { get; set; }

        [Required]
        [StringLength(50)]
        public string Make { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string FuelType { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Colour { get; set; } = null!;

        [Required]
        [Range(0.01, 1000000, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Details { get; set; } = null!;

        public string? ImageUrl { get; set; } = "/images/default.jpg";

        // Not mapped to database: for uploading files
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public virtual ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();
    }
}
