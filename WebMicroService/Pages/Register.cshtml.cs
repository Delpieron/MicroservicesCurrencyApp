using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebMicroService.ViewModels;

namespace WebMicroService.Pages
{
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class RegisterModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        [BindProperty]
        public required RegisterViewModel Register { get; set; }

        public required string ErrorMessage { get; set; }

        public void OnGet()
        {
            // Ewentualna inicjalizacja.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient();
            // Wywo³anie API rejestracji – dostosuj adres URL do swojego œrodowiska
            var response = await client.PostAsJsonAsync("http://restapiservice/api/Login/register", new
            {
                Register.Username,
                Register.Password,
                Register.Email
            });

            if (response.IsSuccessStatusCode)
            {
                // Rejestracja udana – przekieruj u¿ytkownika do strony logowania.
                return RedirectToPage("Login");
            }

            ErrorMessage = "Rejestracja nie powiod³a siê";
            return Page();
        }
    }
}
