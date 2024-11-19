using AutoMapper;
using BikeStore_BackEnd.Data;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore_BackEnd.Iservices.ServicesImpl
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly BikeApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartService(BikeApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the shopping cart items for a specified user.
        /// </summary>
        public async Task<IEnumerable<ShoppingCartDto>> GetShoppingCartItemsByUserIdAsync(int userId)
        {
            var cartItems = await _context.ShoppingCarts
                .Where(sc => sc.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ShoppingCartDto>>(cartItems);
        }

        /// <summary>
        /// Adds an item to the shopping cart.
        /// </summary>
        public async Task<ShoppingCartDto> AddItemToCartAsync(ShoppingCartDto shoppingCartDto)
        {
            var cartItem = _mapper.Map<ShoppingCart>(shoppingCartDto);
            await _context.ShoppingCarts.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<ShoppingCartDto>(cartItem);
        }

        /// <summary>
        /// Updates a specified cart item.
        /// </summary>
        public async Task<ShoppingCartDto> UpdateCartItemAsync(int cartItemId, ShoppingCartDto shoppingCartDto)
        {
            var cartItem = await _context.ShoppingCarts.FindAsync(cartItemId);
            if (cartItem == null) return null;

            _mapper.Map(shoppingCartDto, cartItem);
            _context.ShoppingCarts.Update(cartItem);
            await _context.SaveChangesAsync();

            return _mapper.Map<ShoppingCartDto>(cartItem);
        }

        /// <summary>
        /// Removes a specified item from the shopping cart.
        /// </summary>
        public async Task<bool> RemoveItemFromCartAsync(int cartItemId)
        {
            var cartItem = await _context.ShoppingCarts.FindAsync(cartItemId);
            if (cartItem == null) return false;

            _context.ShoppingCarts.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Calculates the total price of the cart items for a specified user.
        /// </summary>
        public async Task<decimal> GetCartTotalAsync(int userId)
        {
            var cartItems = await _context.ShoppingCarts
                .Where(sc => sc.UserId == userId)
                .ToListAsync();

            var productIds = cartItems.Select(item => item.ProductId).Distinct();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToDictionaryAsync(p => p.ProductId, p => p.Price);

            decimal total = cartItems.Sum(item => item.Quantity * (products.TryGetValue(item.ProductId, out var price) ? price : 0));
            return total;
        }

        /// <summary>
        /// Gets a specific cart item by its ID.
        /// </summary>
        public async Task<ShoppingCartDto> GetCartItemByIdAsync(int cartId)
        {
            var cartItem = await _context.ShoppingCarts.FindAsync(cartId);
            return _mapper.Map<ShoppingCartDto>(cartItem);
        }
    }
}
