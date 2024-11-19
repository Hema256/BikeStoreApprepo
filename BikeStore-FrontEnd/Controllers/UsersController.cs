using BikeStore_FrontEnd.Dto; // Using frontend DTOs
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BikeStore_FrontEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        string _url = "";
        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://localhost:7217/api/users"); // Base API URL

            _url = "https://localhost:7217/api/users";
        }

        // GET: User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO registerDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(registerDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7217/api/User/register", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }
            return View(registerDto);
        }

        // GET: User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
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

        // GET: User/Profile
        public async Task<IActionResult> Profile()
        {
            var response = await _httpClient.GetAsync("profile");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var userDto = JsonConvert.DeserializeObject<UserDTO>(jsonResponse);
                return View(userDto);
            }
            return RedirectToAction("Login");
        }

        // GET: User/UpdateProfile
        public async Task<IActionResult> UpdateProfile()
        {
            var response = await _httpClient.GetAsync("profile");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var userDto = JsonConvert.DeserializeObject<UserDTO>(jsonResponse);
                return View(userDto);
            }
            return RedirectToAction("Login");
        }

        // POST: User/UpdateProfile
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(userDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"profile/{userDto.UserId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Update failed. Please try again.");
                }
            }
            return View(userDto);
        }

        // POST: User/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var response = await _httpClient.PostAsync("logout", null);

            if (response.IsSuccessStatusCode)
            {
                // Perform any necessary session cleanup here
                return RedirectToAction("Login");
            }
            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
