using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using BikeStore_FrontEnd.Dto;

namespace BikeStore_BackEnd.Controllers
{
    public class ReviewController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReviewController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            var reviews = await _httpClient.GetFromJsonAsync<IEnumerable<ReviewDTO>>("https://localhost:7217/api/review");
            return View(reviews);
        }

        // GET: Review/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var review = await _httpClient.GetFromJsonAsync<ReviewDTO>($"https://localhost:7217/api/review/{id}");
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewDTO reviewDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7217/api/review", reviewDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error creating review. Please try again.");
            }
            return View(reviewDto);
        }

        // GET: Review/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _httpClient.GetFromJsonAsync<ReviewDTO>($"https://localhost:7217/api/review/{id}");
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReviewDTO reviewDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync("https://localhost:7217/api/review", reviewDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error updating review. Please try again.");
            }
            return View(reviewDto);
        }

        // GET: Review/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _httpClient.GetFromJsonAsync<ReviewDTO>($"https://localhost:7217/api/review/{id}");
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7217/api/review/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
