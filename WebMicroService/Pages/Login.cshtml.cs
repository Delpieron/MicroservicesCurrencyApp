using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebMicroService.ViewModels;

namespace WebMicroService.Pages
{
    [IgnoreAntiforgeryToken(Order = 2000)]
    public class LoginModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        [BindProperty]
        public required LoginViewModel Login { get; set; }

        public required string ErrorMessage { get; set; }

        public void OnGet()
        {
            // Ewentualna inicjalizacja, np. wyzerowanie p�l.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient();
            // Wywo�anie API logowania � adres dostosuj do swojego �rodowiska
            var response = await client.PostAsJsonAsync("http://restapiservice/api/Login/login", new
            {
                Login.Username,
                Login.Password
            });

            if (response.IsSuccessStatusCode)
            {
                // Logowanie udane � mo�na przekierowa� do chronionej strony lub dashboardu.
                return RedirectToPage("Index"); // Upewnij si�, �e taka strona istnieje lub zmie� na odpowiedni�.
            }

            ErrorMessage = "Niepoprawny login lub has�o";
            return Page();
        }
    }
}
