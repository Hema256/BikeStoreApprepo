using AutoMapper;

using BikeStore_FrontEnd.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BikeStore_FrontEnd.Controllers
{
    [Route("ShoppingCart")]
    public class ShoppingCartController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7217/api/ShoppingCart";

        public ShoppingCartController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: /ShoppingCart/UserCart/{userId}
        [HttpGet("UserCart/{userId}")]
        public async Task<IActionResult> UserCart(int userId)
        {
            var apiUrl = $"{_baseApiUrl}/user/{userId}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var cartItems = JsonSerializer.Deserialize<List<ShoppingCartDto>>(responseBody, options);
                return View(cartItems); // View for displaying user's cart items
            }

            return View("Error");
        }

        // GET: /ShoppingCart/AddToCart
        [HttpGet("AddToCart")]
        public IActionResult AddToCart()
        {
            return View(); // View for adding a new item to the cart
        }

        // POST: /ShoppingCart/AddToCart
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null)
            {
                return BadRequest("Invalid cart item data.");
            }

            var apiUrl = $"{_baseApiUrl}";
            var jsonContent = JsonSerializer.Serialize(shoppingCartDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("UserCart", new { userId = shoppingCartDto.UserId });
            }

            return View("Error", await response.Content.ReadAsStringAsync());
        }

        // GET: /ShoppingCart/UpdateCartItem/{cartItemId}
        [HttpGet("UpdateCartItem/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId)
        {
            var apiUrl = $"{_baseApiUrl}/{cartItemId}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var cartItem = JsonSerializer.Deserialize<ShoppingCartDto>(responseBody, options);
                return View(cartItem); // View for updating the cart item
            }

            return View("Error");
        }

        // POST: /ShoppingCart/UpdateCartItem/{cartItemId}
        [HttpPost("UpdateCartItem/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null)
            {
                return BadRequest("Invalid cart item data.");
            }

            var apiUrl = $"{_baseApiUrl}/{cartItemId}";
            var jsonContent = JsonSerializer.Serialize(shoppingCartDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("UserCart", new { userId = shoppingCartDto.UserId });
            }

            return View("Error", await response.Content.ReadAsStringAsync());
        }

        // GET: /ShoppingCart/RemoveFromCart/{cartItemId}
        [HttpGet("RemoveFromCart/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var apiUrl = $"{_baseApiUrl}/{cartItemId}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var cartItem = JsonSerializer.Deserialize<ShoppingCartDto>(responseBody, options);
                return View(cartItem); // View to confirm item removal
            }

            return View("Error");
        }

        // POST: /ShoppingCart/RemoveFromCart/{cartItemId}
        [HttpPost("RemoveFromCart/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId, int userId)
        {
            var apiUrl = $"{_baseApiUrl}/{cartItemId}";
            var response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("UserCart", new { userId = userId });
            }

            return View("Error", await response.Content.ReadAsStringAsync());
        }

        // GET: /ShoppingCart/CartItemById/{cartId}
        [HttpGet("CartItemById/{cartId}")]
        public async Task<IActionResult> CartItemById(int cartId)
        {
            var apiUrl = $"{_baseApiUrl}/{cartId}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var cartItem = JsonSerializer.Deserialize<ShoppingCartDto>(responseBody, options);
                return View(cartItem); // View to display a single cart item
            }

            return View("Error");
        }

        // GET: /ShoppingCart/CartTotal/{userId}
        [HttpGet("CartTotal/{userId}")]
        public async Task<IActionResult> CartTotal(int userId)
        {
            var apiUrl = $"{_baseApiUrl}/total/user/{userId}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var totalResponse = JsonSerializer.Deserialize<CartTotalResponse>(responseBody, options);

                if (totalResponse != null)
                {
                    ViewData["UserId"] = userId; // Pass userId to the view
                    return View(totalResponse.TotalPrice); // Pass the total price to the view
                }
            }

            return View("Error");
        }
        // GET: /ShoppingCart/Index/{userId}
    }
}
