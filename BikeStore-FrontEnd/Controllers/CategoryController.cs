using BikeStore_FrontEnd.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace BikeStore_FrontEnd.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7217/api/Category") // replace with your actual backend URL
            };
        }

        // GET: Category
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7217/api/Category");
            response.EnsureSuccessStatusCode();
            //var categories = await resp<IEnumerable<CategoryDTO>>("https://localhost:7217/api/order");
            var categories = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDTO>>("https://localhost:7217/api/Category");
            return View(categories);
        }

        // GET: Category/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7217/api/Category/{id}");
            response.EnsureSuccessStatusCode();
            var category = await _httpClient.GetFromJsonAsync<CategoryDTO>($"https://localhost:7217/api/Category/{id}");
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(CategoryDTO category)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/category", category);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7217/api/category/{id}");
            response.EnsureSuccessStatusCode();
            var category = await _httpClient.GetFromJsonAsync<CategoryDTO>($"https://localhost:7217/api/category/{id}");
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, CategoryDTO category)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/category/{id}", category);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7217/api/Category/{ id}");
            response.EnsureSuccessStatusCode();
            var category = await _httpClient.GetFromJsonAsync<CategoryDTO>($"https://localhost:7217/api/Category/{ id}");
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/category/{id}");
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }

}