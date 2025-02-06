using Microsoft.AspNetCore.Mvc;
using WebMicroService.ViewModels;

namespace WebMicroService.Controllers
{
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class LoginController(IHttpClientFactory clientFactory) : Controller
    {
        private readonly IHttpClientFactory _clientFactory = clientFactory;

        // Wyświetlenie widoku logowania (Index.cshtml)
        public IActionResult Index()
        {
            return View();
        }

        // Akcja obsługująca POST dla logowania
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var client = _clientFactory.CreateClient();
            // Adres API – dostosuj do ustawień (np. http://localhost:5001)
            var response = await client.PostAsJsonAsync("http://localhost:5001/api/Login/login", new
            {
                model.Username,
                model.Password
            });

            if (response.IsSuccessStatusCode)
            {
                // Po poprawnym logowaniu możesz przekierować użytkownika, zapisać token itp.
                return RedirectToAction("Index", "Dashboard"); // Przykładowa akcja po logowaniu
            }

            ModelState.AddModelError("", "Niepoprawny login lub hasło");
            return View("Index", model);
        }

        // Wyświetlenie widoku rejestracji (Register.cshtml)
        public IActionResult Register()
        {
            return View();
        }

        // Akcja obsługująca POST dla rejestracji
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("http://localhost:5001/api/Login/register", new
            {
                model.Username,
                model.Password,
                model.Email
            });

            if (response.IsSuccessStatusCode)
            {
                // Po poprawnej rejestracji możesz przekierować użytkownika do strony logowania
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Rejestracja nie powiodła się");
            return View(model);
        }
    }
}
