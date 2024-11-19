using System.ComponentModel.DataAnnotations;

namespace BikeStore_FrontEnd.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username can't be longer than 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }  // Example: "Admin", "Customer"

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? LastLoginDate { get; set; }

        // Navigation Properties
        //public ICollection<Order> Orders { get; set; }
        //public ICollection<Review> Reviews { get; set; }
        //public ICollection<ShoppingCart> ShoppingCartItems { get; set; }
    }
}