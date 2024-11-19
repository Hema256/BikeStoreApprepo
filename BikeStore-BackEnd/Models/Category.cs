using System.ComponentModel.DataAnnotations;

namespace BikeStore_BackEnd.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string CategoryName { get; set; }

        // Navigation Properties
        public ICollection<Product> Products { get; set; }
    }
}