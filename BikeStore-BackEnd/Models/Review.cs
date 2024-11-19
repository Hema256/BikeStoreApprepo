using System;
using System.ComponentModel.DataAnnotations;

namespace BikeStore_BackEnd.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }         // Primary Key

        [Required(ErrorMessage = "Bike ID is required.")]
        public int BikeId { get; set; }            // Foreign Key to Product

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }            // Foreign Key to User

        [Required(ErrorMessage = "Review content is required.")]
        [StringLength(1000, ErrorMessage = "Review content cannot exceed 1000 characters.")]
        public string Content { get; set; }         // Review text

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }             // Rating out of 5

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp of when the review was created

        // Navigation Properties
        public virtual Product Product { get; set; }   // Navigation property to Product
        public virtual User User { get; set; }
    }
}
