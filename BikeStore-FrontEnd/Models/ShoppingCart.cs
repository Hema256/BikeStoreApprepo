using System.ComponentModel.DataAnnotations;

namespace BikeStore_FrontEnd.Models
{
    public class ShoppingCart
    {
        
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Navigation Properties
        //public User User { get; set; }
        //public Product Product { get; set; }
    }

}
