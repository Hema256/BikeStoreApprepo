using System.ComponentModel.DataAnnotations;

namespace BikeStore_BackEnd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Total price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be greater than zero.")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Order status is required.")]
        [StringLength(50, ErrorMessage = "Order status cannot exceed 50 characters.")]
        public string OrderStatus { get; set; }  // Example: "Pending", "Shipped", "Delivered"

        // Navigation Properties
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
