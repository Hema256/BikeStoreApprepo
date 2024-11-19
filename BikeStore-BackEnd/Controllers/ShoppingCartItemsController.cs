using AutoMapper;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCart : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public ShoppingCart(IShoppingCartService shoppingCartService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }

        // GET: api/shoppingcartitems/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetShoppingCartItemsByUserId(int userId)
        {
            var cartItems = await _shoppingCartService.GetShoppingCartItemsByUserIdAsync(userId);
            return Ok(cartItems);
        }

        // POST: api/shoppingcartitems
        [HttpPost]
        public async Task<IActionResult> AddItemToCart([FromBody] ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null)
            {
                return BadRequest("Invalid cart item data.");
            }

            var addedItem = await _shoppingCartService.AddItemToCartAsync(shoppingCartDto);
            return CreatedAtAction(nameof(GetCartItemById), new { cartId = addedItem.CartId }, addedItem);
        }

        // PUT: api/shoppingcartitems/{cartItemId}
        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, [FromBody] ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null)
            {
                return BadRequest("Invalid cart item data.");
            }

            var updatedItem = await _shoppingCartService.UpdateCartItemAsync(cartItemId, shoppingCartDto);
            if (updatedItem == null)
            {
                return NotFound($"Cart item with ID {cartItemId} not found.");
            }

            return Ok(updatedItem);
        }

        // DELETE: api/shoppingcartitems/{cartItemId}
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            var result = await _shoppingCartService.RemoveItemFromCartAsync(cartItemId);
            if (!result)
            {
                return NotFound($"Cart item with ID {cartItemId} not found.");
            }

            return NoContent(); // Successfully deleted
        }

        // GET: api/shoppingcartitems/cart/{cartId}
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartItemById(int cartId)
        {
            var cartItem = await _shoppingCartService.GetCartItemByIdAsync(cartId);
            if (cartItem == null)
            {
                return NotFound($"Cart item with ID {cartId} not found.");
            }

            return Ok(cartItem);
        }

        // GET: api/shoppingcartitems/total/user/{userId}
        [HttpGet("total/user/{userId}")]
        public async Task<IActionResult> GetCartTotal(int userId)
        {
            var total = await _shoppingCartService.GetCartTotalAsync(userId);
            return Ok(new { TotalPrice = total });
        }
    }
}
