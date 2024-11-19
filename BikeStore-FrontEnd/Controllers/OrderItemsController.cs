using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; // Newtonsoft for serialization/deserialization
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BikeStore_FrontEnd.Dto;
using System.Text.Json; // For new serialization methods
using System.Text.Json.Serialization;

namespace BikeStore_BackEnd.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderItemsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: OrderItemsMvc/Order/{orderId}
        public async Task<IActionResult> Index(int orderId)
        {
            var response = await _httpClient.GetAsync($"api/orderitems/order/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                // Deserialize list of OrderItemDto
                var orderItems = JsonConvert.DeserializeObject<IEnumerable<OrderItemDto>>(jsonData);
                return View(orderItems);
            }
            return NotFound();
        }
      
        [HttpGet]
        // GET: OrderItemsMvc/Details/{orderItemId}
        public async Task<IActionResult> Details(int orderItemId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7217/api/OrderItems/order/{orderItemId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                // Deserialize a single OrderItemDto object
                var orderItem = JsonConvert.DeserializeObject<List<OrderItemDto>>(jsonData);
                return View(orderItem[0]);
            }
            return NotFound();
        }

        // GET: OrderItemsMvc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderItemsMvc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderItemDto orderItemDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(orderItemDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/orderitems", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { orderId = orderItemDto.OrderId });
                }
            }
            return View(orderItemDto);
        }

        // GET: OrderItemsMvc/Edit/{orderItemId}
        public async Task<IActionResult> Edit(int orderItemId)
        {
            var response = await _httpClient.GetAsync($"api/orderitems/Edit/{orderItemId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var orderItem = JsonConvert.DeserializeObject<OrderItemDto>(jsonData);
                return View(orderItem);
            }
            return NotFound();
        }

        // POST: OrderItemsMvc/Edit/{orderItemId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int orderItemId, OrderItemDto orderItemDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(orderItemDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/orderitems/{orderItemId}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { orderId = orderItemDto.OrderId });
                }
            }
            return View(orderItemDto);
        }

        // POST: OrderItemsMvc/Delete/{orderItemId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int orderItemId)
        {
            var response = await _httpClient.DeleteAsync($"api/orderitems/{orderItemId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
