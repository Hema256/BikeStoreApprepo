using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BikeStore_FrontEnd.Dto;

namespace BikeStore_BackEnd.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var orders = await _httpClient.GetFromJsonAsync<IEnumerable<OrderDTO>>("https://localhost:7217/api/order");
            return View(orders);
        }

        // GET: Order/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var order = await _httpClient.GetFromJsonAsync<OrderDTO>($"https://localhost:7217/api/order/{id}");
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDTO orderDto)
        {
            orderDto.TotalPrice = 50000;
            orderDto.OrderStatus = "success";
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7217/api/order", orderDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Error creating order. Please try again.");

            return RedirectToAction("OrderIndex", "Order");
        }

        // GET: Order/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _httpClient.GetFromJsonAsync<OrderDTO>($"https://localhost:7217/api/order/{id}");
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderDTO orderDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync("https://localhost:7217/api/order", orderDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error updating order. Please try again.");
            }
            return View(orderDto);
        }

        // GET: Order/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _httpClient.GetFromJsonAsync<OrderDTO>($"https://localhost:7217/api/order/{id}");
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7217/api/order/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> OrderIndex()
        {
            var orders = await _httpClient.GetFromJsonAsync<IEnumerable<OrderDTO>>("https://localhost:7217/api/order");
            if (orders == null)
            {
                return NotFound("No orders found.");
            }
            return View(orders);
        }

    }
}
