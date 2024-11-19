using System.ComponentModel.DataAnnotations;

namespace BikeStore_BackEnd.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }  // Primary Key

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }  // Foreign Key to User

        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }  // Foreign Key to Product

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }  // Quantity of the product in the cart

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
