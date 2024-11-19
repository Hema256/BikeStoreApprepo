using BikeStore_FrontEnd.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BikeStore_FrontEnd.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7217/api/users"); // Base API URL
        }
        // GET: Admin/Index (Admin Dashboard)
        public IActionResult Index()
        {
            // Here you can include logic to display admin-specific data on the dashboard.
            return View();
        }

        // GET: Admin/Login
        public IActionResult Login()
        {
            // This will show the admin login form.
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDTO loginDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(loginDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("login", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // Deserialize the result to extract token or user info (if returned)
                    // You can store the token in session if required
                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View(loginDto);
        }
        public async Task<IActionResult> AdminLogin(UserLoginDTO loginDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(loginDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("login", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // Deserialize the result to extract token or user info (if returned)
                    // You can store the token in session if required
                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View(loginDto);
        }


    }
}