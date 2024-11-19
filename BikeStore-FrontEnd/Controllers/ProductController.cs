using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
//using BikeStore_MVC.Models;
using BikeStore_FrontEnd.Dto;
// Ensure this is the correct namespace for your ProductDTO

namespace BikeStore_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7217/api/Product/"); // Web API base URL
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case sensitivity
            };

            var products = JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(responseData, options);

            return View(products);
        }

        // GET: Product/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case sensitivity
            };

            var product = JsonSerializer.Deserialize<ProductDTO>(responseData,options);

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("", content);

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<ProductDTO>(responseData);

            return View(product);
        }

        // POST: Product/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDTO product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<ProductDTO>(responseData);

            return View(product);
        }

        // POST: Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/ProductIndex
        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            var response = await _httpClient.GetAsync(""); // Assuming this retrieves all products
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case sensitivity
            };

            var products = JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(responseData, options);

            return View(products);
        }



    }
}
