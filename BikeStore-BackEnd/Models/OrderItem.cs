﻿using System.ComponentModel.DataAnnotations;

namespace BikeStore_BackEnd.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "Order ID is required.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }  // Price at the time of the order

        // Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
