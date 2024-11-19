using BikeStore_BackEnd.Dto;

namespace BikeStore_BackEnd.Iservices
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartDto>> GetShoppingCartItemsByUserIdAsync(int userId);
        Task<ShoppingCartDto> AddItemToCartAsync(ShoppingCartDto shoppingCartDto);
        Task<ShoppingCartDto> UpdateCartItemAsync(int cartItemId, ShoppingCartDto shoppingCartDto);
        Task<bool> RemoveItemFromCartAsync(int cartItemId);
        Task<decimal> GetCartTotalAsync(int userId);
        Task<ShoppingCartDto> GetCartItemByIdAsync(int cartId); // Fixed return type
    }
}