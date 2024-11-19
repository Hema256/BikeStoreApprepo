using System.ComponentModel.DataAnnotations;

namespace BikeStore_FrontEnd.Models
{
    public class Order
    {
        
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }  // Example: "Pending", "Shipped", "Delivered"

        // Navigation Properties
       /* public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }*/
    }

}
