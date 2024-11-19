using System.ComponentModel.DataAnnotations;

namespace BikeStore_FrontEnd.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }  // Price at the time of the order

       // Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}
