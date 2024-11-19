using System.ComponentModel.DataAnnotations;

namespace BikeStore_FrontEnd.Dto
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }
    }

    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int BikeId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
    }
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
    }

  

    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class ShoppingCartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CartTotalResponse
    {
        public Decimal TotalPrice { get; set; }
    }
    public class UserRegisterDTO
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        //public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
    public class UserLoginDTO
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be at least 8 characters long and can't be longer than 100 characters.", MinimumLength = 6)]
        public string PasswordHash { get; set; }
    }
    public class UserLogoutDTO
    {
        public int UserId { get; set; }
    }
}

