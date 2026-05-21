using System.Text;
using System.Text.Json;
using CostaRicaMusic.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaMusic.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Revise los datos ingresados.";
                return View(model);
            }

            var loginData = new
            {
                Correo = model.Correo,
                Password = model.Contrasena
            };

            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7232/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var loginResponse = JsonSerializer.Deserialize<LoginResponseViewModel>(responseContent, options);

                if (loginResponse != null)
                {
                    HttpContext.Session.SetString("UserId", loginResponse.UserId.ToString());
                    HttpContext.Session.SetString("Nombre", loginResponse.Nombre);
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas o el API no respondió bien.";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}