using System.ComponentModel.DataAnnotations;

namespace BikeStore_FrontEnd.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }         // Primary Key
        public int BikeId { get; set; }            // Foreign Key to Product
        public int UserId { get; set; }            // Foreign Key to User
        public string Content { get; set; }         // Review text
        public int Rating { get; set; }             // Rating out of 5
        public DateTime CreatedAt { get; set; }     // Timestamp of when the review was created

        // Navigation Properties
        //public virtual Product Product { get; set; }   // Navigation property to Product
        //public virtual User User { get; set; }
    }
}
