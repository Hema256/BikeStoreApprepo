using System.ComponentModel.DataAnnotations;

namespace BikeStore_FrontEnd.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int StockQuantity { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
        public decimal Rating { get; set; }

        // Navigation Properties
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ShoppingCart> ShoppingCartItems { get; set; }
    }

}
